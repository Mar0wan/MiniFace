using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class Friendship
    {
        public string? UserId { get; set; }
        public User User { get; set; }

        public string? UserFriendId { get; set; }
        public User UserFriend { get; set; }
    }
}
