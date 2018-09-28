using System;

namespace SandBox.Dtos
{
    public class PostDto
    {
        public int Id { get; private set; }
        public string Title { get; set; }
        public string Contents { get; set; }
        public DateTime DatePublished { get; set; }
        public ApplicationUserDto Publisher { get; set; }
        public DateTime? LastTimeEdited { get; private set; }
        public int NumberOfEdits { get; set; }

    }
}