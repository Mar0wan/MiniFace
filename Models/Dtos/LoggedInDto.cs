using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Dtos
{
    public class LoggedInDto
    {
        public string Token { get; set; }
        public string Role { get; set; }
    }
}
