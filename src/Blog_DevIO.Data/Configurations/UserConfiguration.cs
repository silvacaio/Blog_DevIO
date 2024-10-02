using Blog_DevIO.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog_DevIO.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).HasColumnType("varchar(100)").IsRequired();
            builder.Property(p => p.LastName).HasColumnType("varchar(100)").IsRequired();
            builder.Property(p => p.NickName).HasColumnType("varchar(100)").IsRequired();
            builder.Property(p => p.Age).IsRequired();
            builder.Property(p => p.Creation).HasDefaultValueSql("getdate()").ValueGeneratedOnAdd();

            builder.HasMany(p => p.Posts)
            .WithOne(p => p.User)
            .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Comments)
             .WithOne(p => p.User)
             .OnDelete(DeleteBehavior.Cascade);
        }
    }
}