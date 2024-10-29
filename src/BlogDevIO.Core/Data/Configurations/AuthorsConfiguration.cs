using Blog_DevIO.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog_DevIO.Data.Configurations
{
    public class AuthorsConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.ToTable("Authors");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnType("varchar(100)").IsRequired();
            builder.Property(p => p.FistName).HasColumnType("varchar(100)").IsRequired();
            builder.Property(p => p.LastName).HasColumnType("varchar(100)").IsRequired();
            builder.Property(p => p.Creation).HasDefaultValueSql("getdate()").ValueGeneratedOnAdd();

            builder.HasMany(p => p.Posts)
             .WithOne(p => p.Author)
             .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(p => p.Comments)
             .WithOne(p => p.Author)
             .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
