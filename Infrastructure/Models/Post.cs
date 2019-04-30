using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models
{
    public class Post
    {
        public int Id { get; private set; }

        [Required]
        [StringLength(40)]
        public string Title { get; set; }

        [Required]
        public string Contents { get; set; }

        [Required]
        public DateTime DatePublished { get; set; }

        public ApplicationUser Publisher { get; set; }

        [Required]
        public string PublisherId { get; set; }

        public DateTime? LastTimeEdited { get; private set; }

        public int NumberOfEdits { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public void Edit(string title, string contents)
        {
            Title = title;
            Contents = contents;
            LastTimeEdited = DateTime.Now;
            NumberOfEdits++;
        }
    }
}