using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class PostModel
    {
        public string? Text { get; set; }
        public IFormFile Image { get; set; }
    }
}
