using Blog_DevIO.Core.Entities;
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
            builder.Property(p => p.AuthorId).IsRequired();
            builder.Property(p => p.Id).HasColumnType("varchar(100)").IsRequired();
            builder.Property(p => p.Content).HasColumnType("BLOB").IsRequired();
            builder.Property(p => p.Creation).HasDefaultValueSql("getdate()").ValueGeneratedOnAdd();

            builder.HasMany(p => p.Comments)
            .WithOne(p => p.Post)
            .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.Author)
                .WithMany(p => p.Posts)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
