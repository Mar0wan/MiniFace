using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Configurations
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Id).HasMaxLength(100).HasDefaultValueSql("NEWID()");
            builder.Property(u => u.Email).HasMaxLength(100);
            builder.Property(u => u.UserName).HasMaxLength(256);
            builder.Property(u => u.SecurityStamp).HasMaxLength(256);
            builder.Property(u => u.PhoneNumber).HasMaxLength(15);
            builder.Property(u => u.NormalizedUserName).HasMaxLength(256);
            builder.Property(u => u.NormalizedEmail).HasMaxLength(100);
            builder.HasMany(u => u.UserRole)
                .WithOne()
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();

            builder.HasMany(u => u.Posts).WithOne(e=>e.User)
                .HasForeignKey(fk => fk.UserId).IsRequired();


        }
    }
}
