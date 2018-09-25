using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SandBox.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public ApplicationUser CommentingUser { get; set; }

        public string CommentingUserId { get; set; }

        public Post Post { get; set; }

        public int PostId { get; set; }

        [Required]
        [StringLength(255)]
        public string Contents { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }
    }
}