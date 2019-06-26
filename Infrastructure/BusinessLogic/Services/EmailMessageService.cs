using Infrastructure.BusinessLogic.Interfaces;
using Infrastructure.DataAccess.Interfaces;
using Infrastructure.Dtos;
using Infrastructure.Models;
using System;

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
                SenderId = messageDto.Receiver.Id,
                RequestDeliveryNote = messageDto.RequestDeliveryNote,
                Message = messageDto.Message,
                Title = messageDto.Title
            };

            _messageRepository.SaveAndSendMessage(message);

            return new ResultDto() { Message = "E-mail has been sent", RequestStatus = RequestStatus.Success };
        }
    }
}
