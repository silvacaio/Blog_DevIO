﻿using Blog_DevIO.Core.Entities;
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
            builder.Property(p => p.AuthorId).IsRequired();
            builder.Property(p => p.Content).HasColumnType("BLOB").IsRequired();
            builder.Property(p => p.Creation).IsRequired();

            builder.HasOne(p => p.Post)
                    .WithMany(p => p.Comments)
                    .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.Author)
                .WithMany(p => p.Comments)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
