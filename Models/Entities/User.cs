using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class User:IdentityUser
    {
        public virtual ICollection<IdentityUserRole<string>> UserRole { get; set; }

        public ICollection<Post> Posts { get; set; }

        public ICollection<Friendship> FriendsOf { get; set; }
        public ICollection<Friendship> Friends { get; set; }

    }
}
