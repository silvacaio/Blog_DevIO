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
            modelBuilder.Entity<User>(p => 
            {
                p.ToTable("Users");
                p.HasKey(p => p.Id);
                p.Property(p => p.Name).HasColumnType("varchar(100)").IsRequired();
                p.Property(p => p.LastName).HasColumnType("varchar(100)").IsRequired();
                p.Property(p => p.NickName).HasColumnType("varchar(100)").IsRequired();
                p.Property(p => p.Age).IsRequired();
                p.Property(p => p.Creation).HasDefaultValueSql("getdate()").ValueGeneratedOnAdd();

                p.HasMany(p => p.Posts)
                .WithOne(p => p.User)
                .OnDelete(DeleteBehavior.Cascade);

                p.HasMany(p => p.Comments)
                 .WithOne(p => p.User)
                 .OnDelete(DeleteBehavior.Cascade);
            });


            modelBuilder.Entity<Post>(p =>
            {
                p.ToTable("Posts");
                p.HasKey(p => p.Id);
                p.Property(p => p.Id).HasColumnType("varchar(100)").IsRequired();
                p.Property(p => p.Content).HasColumnType("varchar(max)").IsRequired();
                p.Property(p => p.Creation).HasDefaultValueSql("getdate()").ValueGeneratedOnAdd();

                p.HasMany(p => p.Comments)
                .WithOne(p => p.Post)
                .OnDelete(DeleteBehavior.Cascade);

                p.HasOne(p => p.User)
                 .WithMany(p => p.Posts)
                 .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Comment>(p =>
            {
                p.ToTable("Comments");
                p.HasKey(p => p.Id);
                p.Property(p => p.Content).HasColumnType("varchar(max)").IsRequired();
                p.Property(p => p.Creation).HasDefaultValueSql("getdate()").ValueGeneratedOnAdd();

                p.HasOne(p => p.Post)
                    .WithMany(p => p.Comments)
                    .OnDelete(DeleteBehavior.Cascade);

                p.HasOne(p => p.User)
                 .WithMany(p => p.Comments)
                 .OnDelete(DeleteBehavior.NoAction);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
