using Company.PostsAndCommentsModels.DatabaseModels;
using Microsoft.EntityFrameworkCore;

namespace Company.PostsAndCommentsRepositories.DbContext
{
    public class PacDbContext: Microsoft.EntityFrameworkCore.DbContext
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
