using SandBox.Models;
using System.Collections.Generic;

namespace SandBox.ViewModels
{
    public class PostWithCommentsViewModel
    {
        public PostWithCommentsViewModel()
        {
            Comments = new List<Comment>();
        }

        public Post Post { get; set; }
        public List<Comment> Comments { get; set; }
    }
}