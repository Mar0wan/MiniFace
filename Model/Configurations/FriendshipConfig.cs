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
    public class FriendshipConfig : IEntityTypeConfiguration<Friendship>
    {
        public void Configure(EntityTypeBuilder<Friendship> builder)
        {
            builder.HasKey(x => new { u1 = x.UserId, u2 = x.UserFriendId });

            builder.HasOne(x => x.User)
                .WithMany(x => x.Friends)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.UserFriend)
                .WithMany(x => x.FriendsOf)
                .HasForeignKey(x => x.UserFriendId)
                .OnDelete(DeleteBehavior.Restrict);

            //builder.HasKey(x => new { x.UserId, x.UserFriendId });

            //builder.HasOne(x => x.User)
            //    .WithMany(x => x.Friends)
            //    .HasForeignKey(x => x.UserId);

            //builder.HasOne(x => x.UserFriend)
            //    .WithMany(x => x.FriendsOf)
            //    .HasForeignKey(x => x.UserFriendId);

        }
    }
}
