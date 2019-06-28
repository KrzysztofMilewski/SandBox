using Infrastructure.Models;
using System.Linq;

namespace Infrastructure.DataAccess.Interfaces
{
    public interface IEmailMessageRepository
    {
        void SaveAndSendMessage(EmailMessage message);
        IQueryable<EmailMessage> GetIncomingMessages(string userId);
        EmailMessage GetMessage(int id);
        void MarkAsRead(EmailMessage message);
        int GetNumberOfUnreadMessages(string userId);
    }
}
