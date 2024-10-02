using Blog_DevIO.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog_DevIO.Data
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options) { }
        public DbSet<Post> Post { get; set; }
        public DbSet<Comment> Comment { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BlogContext).Assembly);          

            base.OnModelCreating(modelBuilder);
        }
    }
}
