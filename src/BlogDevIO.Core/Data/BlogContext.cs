using Blog_DevIO.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Blog_DevIO.Core.Data
{
    public class BlogContext : IdentityDbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options) { }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BlogContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            bool hasProperty(string propertyName, EntityEntry entry) => entry.Entity.GetType().GetProperty(propertyName) != null;

            foreach (var entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Deleted:
                        if (hasProperty("IsDeleted", entry))
                        {
                            entry.Property("IsDeleted").CurrentValue = true;
                            entry.State = EntityState.Modified;
                        }
                        break;

                    case EntityState.Modified:
                        if (hasProperty("Creation", entry))
                            entry.Property("Creation").IsModified = false;
                        break;

                    case EntityState.Added:
                        if (hasProperty("Creation", entry))
                            entry.Property("Creation").CurrentValue = DateTime.Now;
                        break;

                    case EntityState.Unchanged:
                    case EntityState.Detached:
                    default:
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }


    }
}
