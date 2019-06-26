using System;

namespace Infrastructure.Dtos
{
    public class EmailMessageDto
    {
        public int Id { get; set; }

        public ApplicationUserDto Sender { get; set; }
        public ApplicationUserDto Receiver { get; set; }

        public string Title { get; set; }
        public string Message { get; set; }

        public DateTime DateSent { get; set; }
        public bool IsRead { get; set; }

        public bool RequestDeliveryNote { get; set; }
    }
}
