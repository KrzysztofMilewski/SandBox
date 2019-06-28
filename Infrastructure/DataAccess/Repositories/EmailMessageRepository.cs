using Infrastructure.DataAccess.Interfaces;
using Infrastructure.Models;
using System.Data.Entity;
using System.Linq;

namespace Infrastructure.DataAccess.Repositories
{
    public class EmailMessageRepository : IEmailMessageRepository
    {
        private readonly DbContext _context;
        private readonly DbSet<EmailMessage> _emails;

        public EmailMessageRepository(DbContext context)
        {
            _context = context;
            _emails = context.Set<EmailMessage>();
        }

        public void SaveAndSendMessage(EmailMessage message)
        {
            _emails.Add(message);
            _context.SaveChanges();
        }

        public IQueryable<EmailMessage> GetIncomingMessages(string userId)
        {
            var messages = _emails.Where(em => em.ReceiverId == userId).Include(em => em.Sender).OrderByDescending(em => em.DateSent);
            return messages;
        }

        public EmailMessage GetMessage(int id)
        {
            return _emails.Include(em => em.Sender).SingleOrDefault(em => em.Id == id);
        }

        public void MarkAsRead(EmailMessage message)
        {
            message.IsRead = true;
            _context.SaveChanges();
        }

        public int GetNumberOfUnreadMessages(string userId)
        {
            return _emails.Where(em => em.ReceiverId == userId && !em.IsRead).Count();
        }
    }
}
