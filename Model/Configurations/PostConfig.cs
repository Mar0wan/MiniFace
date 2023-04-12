using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Configurations
{
    public class PostConfig : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            //builder.HasKey(x => x.Id);
            //builder.Property(p => p.Text).IsRequired().HasMaxLength(300);
            //builder.Property(p => p.Image).IsRequired();
            //builder.HasOne(p => p.UserId)
            //    .WithMany().HasForeignKey(fk => fk.UserId);
        }
    }
}
