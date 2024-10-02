using Blog_DevIO.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog_DevIO.Data.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("Posts");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnType("varchar(100)").IsRequired();
            builder.Property(p => p.Content).HasColumnType("varchar(max)").IsRequired();
            builder.Property(p => p.Creation).HasDefaultValueSql("getdate()").ValueGeneratedOnAdd();

            builder.HasMany(p => p.Comments)
            .WithOne(p => p.Post)
            .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.User)
             .WithMany(p => p.Posts)
             .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
