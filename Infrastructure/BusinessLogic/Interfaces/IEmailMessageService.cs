using Infrastructure.Dtos;

namespace Infrastructure.BusinessLogic.Interfaces
{
    public interface IEmailMessageService
    {
        ResultDto SendMessage(EmailMessageDto messageDto);
    }
}
