using Microsoft.EntityFrameworkCore;
using PostsAndCommentsModels.DatabaseModels;

namespace PostsAndCommentsRepositories.DBContext
{
    public class PacDbContext: DbContext
    {
        public PacDbContext()
        {
        }

        public PacDbContext(DbContextOptions<PacDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Image> Images { get; set; }
    }
}
