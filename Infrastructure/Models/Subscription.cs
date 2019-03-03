using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Persistence
{
    public class Subscription
    {
        public ApplicationUser Publisher { get; set; }
        public ApplicationUser Subscriber { get; set; }

        [Key]
        [Column(Order = 1)]
        public string PublisherId { get; set; }

        [Key]
        [Column(Order = 2)]
        public string SubscriberId { get; set; }
    }
}