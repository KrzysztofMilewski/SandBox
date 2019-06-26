using Infrastructure.DataAccess.Interfaces;
using Infrastructure.Models;
using System.Data.Entity;

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
    }
}
