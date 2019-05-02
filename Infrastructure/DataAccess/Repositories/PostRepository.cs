using Infrastructure.DataAccess.Interfaces;
using Infrastructure.Models;
using System.Data.Entity;
using System.Linq;

namespace Infrastructure.DataAccess.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly DbContext _context;
        private readonly DbSet<Post> _posts;

        public PostRepository(DbContext context)
        {
            _context = context;
            _posts = _context.Set<Post>();
        }

        public Post GetPostById(int id)
        {
            var post = _posts.SingleOrDefault(p => p.Id == id);
            return post;
        }

        public IQueryable<Post> GetPostsFromUser(string userId)
        {
            var posts = _posts.Where(p => p.PublisherId == userId);
            return posts;
        }

        public void AddPost(Post postToAdd)
        {
            _posts.Add(postToAdd);
            _context.SaveChanges();
        }

        public void EditPost(Post postToEdit)
        {
            _context.Entry(postToEdit).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeletePost(Post postToDelete)
        {
            _context.Entry(postToDelete).State = EntityState.Deleted;
            _context.SaveChanges();
        }
    }
}
