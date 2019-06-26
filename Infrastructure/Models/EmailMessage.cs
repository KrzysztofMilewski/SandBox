using System;

namespace Infrastructure.Models
{
    public class EmailMessage
    {
        public int Id { get; set; }

        public ApplicationUser Sender { get; set; }
        public ApplicationUser Receiver { get; set; }

        public string SenderId { get; set; }
        public string ReceiverId { get; set; }

        public string Title { get; set; }
        public string Message { get; set; }

        public DateTime DateSent { get; set; }
        public bool IsRead { get; set; }

        public bool RequestDeliveryNote { get; set; }
    }
}
