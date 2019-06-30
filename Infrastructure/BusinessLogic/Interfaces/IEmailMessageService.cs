using Infrastructure.Dtos;
using System.Collections.Generic;

namespace Infrastructure.BusinessLogic.Interfaces
{
    public interface IEmailMessageService
    {
        ResultDto SendMessage(EmailMessageDto messageDto);
        ResultDto<IEnumerable<EmailMessageDto>> GetIncomingMessages(string userId);
        ResultDto<IEnumerable<EmailMessageDto>> GetOutcomingMessages(string userId);
        ResultDto<EmailMessageDto> GetMessage(int messageId, string requestingUserId);
        ResultDto<int> GetNumberOfUnreadMessages(string userId);
        ResultDto SendConfirmation(int messageId, string requestingUserId);
    }
}
