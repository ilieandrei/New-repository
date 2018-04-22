using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Entities
{
    public class User : Entity<Guid>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
