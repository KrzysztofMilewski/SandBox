using System;

namespace Infrastructure.Dtos
{
    public class CommentDto
    {
        public int Id { get; set; }
        public ApplicationUserDto CommentingUser { get; set; }
        public string CommentingUserId { get; set; }
        public int PostId { get; set; }
        public string Contents { get; set; }
        public DateTime DateAdded { get; set; }
    }
}