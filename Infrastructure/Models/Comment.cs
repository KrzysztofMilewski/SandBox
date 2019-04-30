using System;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public ApplicationUser CommentingUser { get; set; }

        [Required]
        public string CommentingUserId { get; set; }

        public Post Post { get; set; }

        [Required]
        public int PostId { get; set; }

        [Required]
        [StringLength(255)]
        public string Contents { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }

        public void Edit(string contents)
        {
            Contents = contents;
        }
    }
}