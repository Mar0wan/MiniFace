using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dtos
{
    public class PostDto
    {

        public string? Text { get; set; }
        public object Image { get; set; }
    }
}
