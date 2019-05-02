using System;

namespace Infrastructure.Dtos
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Contents { get; set; }
        public DateTime DatePublished { get; set; }
        public ApplicationUserDto Publisher { get; set; }
        public DateTime? LastTimeEdited { get; set; }
        public int NumberOfEdits { get; set; }

    }
}