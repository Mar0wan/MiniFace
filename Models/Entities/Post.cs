using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class Post
    {
        [Key]
        public string Id { get; set; }
        public string Text { get; set; }
        public string ImageTitle { get; set; }
        public byte[] Image { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

    }
}
