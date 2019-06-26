using Infrastructure.Models;

namespace Infrastructure.DataAccess.Interfaces
{
    public interface IEmailMessageRepository
    {
        void SaveAndSendMessage(EmailMessage message);
    }
}
