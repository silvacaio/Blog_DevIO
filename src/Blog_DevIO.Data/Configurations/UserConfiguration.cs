using Blog_DevIO.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog_DevIO.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(p => p.FistName).HasColumnType("varchar(100)").IsRequired();
            builder.Property(p => p.LastName).HasColumnType("varchar(100)").IsRequired();

            builder.HasMany(p => p.Posts)
             .WithOne(p => p.User)
             .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(p => p.Comments)
             .WithOne(p => p.User)
             .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
