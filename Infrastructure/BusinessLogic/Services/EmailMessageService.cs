using AutoMapper;
using Infrastructure.BusinessLogic.Interfaces;
using Infrastructure.DataAccess.Interfaces;
using Infrastructure.Dtos;
using Infrastructure.Models;
using System;
using System.Collections.Generic;

namespace Infrastructure.BusinessLogic.Services
{
    public class EmailMessageService : IEmailMessageService
    {
        private readonly IEmailMessageRepository _messageRepository;

        public EmailMessageService(IEmailMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public ResultDto SendMessage(EmailMessageDto messageDto)
        {
            if (messageDto.Sender.Id == messageDto.Receiver.Id)
                return new ResultDto() { Message = "You cannot send e-mail to yourself", RequestStatus = RequestStatus.Error };

            var message = new EmailMessage()
            {
                DateSent = DateTime.Now,
                IsRead = false,
                ReceiverId = messageDto.Receiver.Id,
                SenderId = messageDto.Sender.Id,
                RequestDeliveryNote = messageDto.RequestDeliveryNote,
                Message = messageDto.Message,
                Title = messageDto.Title
            };

            _messageRepository.SaveAndSendMessage(message);

            return new ResultDto() { Message = "E-mail has been sent", RequestStatus = RequestStatus.Success };
        }

        public ResultDto<IEnumerable<EmailMessageDto>> GetIncomingMessages(string userId)
        {
            var messages = _messageRepository.GetIncomingMessages(userId);
            return new ResultDto<IEnumerable<EmailMessageDto>>()
            {
                Data = Mapper.Map<IEnumerable<EmailMessageDto>>(messages),
                RequestStatus = RequestStatus.Success
            };
        }

        public ResultDto<EmailMessageDto> GetMessage(int messageId, string requestingUserId)
        {
            var message = _messageRepository.GetMessage(messageId);

            if (message == null)
                return new ResultDto<EmailMessageDto>() { RequestStatus = RequestStatus.NotFound, Message = "Couldn't find specified message" };

            if (message.ReceiverId != requestingUserId && message.SenderId != requestingUserId)
                return new ResultDto<EmailMessageDto>() { RequestStatus = RequestStatus.NotAuthorized, Message = "No permission to view this messagbe" };

            if (!message.IsRead)
                _messageRepository.MarkAsRead(message);

            return new ResultDto<EmailMessageDto>() { RequestStatus = RequestStatus.Success, Data = Mapper.Map<EmailMessageDto>(message) };
        }

        public ResultDto<int> GetNumberOfUnreadMessages(string userId)
        {
            var unreadMessages = _messageRepository.GetNumberOfUnreadMessages(userId);
            return new ResultDto<int>() { RequestStatus = RequestStatus.Success, Data = unreadMessages };
        }

        public ResultDto<IEnumerable<EmailMessageDto>> GetOutcomingMessages(string userId)
        {
            var messages = _messageRepository.GetOutcomingMessages(userId);

            return new ResultDto<IEnumerable<EmailMessageDto>>
            {
                RequestStatus = RequestStatus.Success,
                Data = Mapper.Map<IEnumerable<EmailMessageDto>>(messages)
            };
        }
    }
}
