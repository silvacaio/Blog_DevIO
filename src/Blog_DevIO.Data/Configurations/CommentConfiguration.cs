using Blog_DevIO.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog_DevIO.Data.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.UserId).IsRequired();
            builder.Property(p => p.Content).HasColumnType("varchar(max)").IsRequired();
            builder.Property(p => p.Creation).HasDefaultValueSql("getdate()").ValueGeneratedOnAdd();

            builder.HasOne(p => p.Post)
                    .WithMany(p => p.Comments)
                    .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.User)
                .WithMany(p => p.Comments)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
