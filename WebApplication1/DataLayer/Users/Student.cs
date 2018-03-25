using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Users
{
    public class Student : Entity<Guid>
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Year { get; set; }
        public string Group { get; set; }
        public User User { get; set; }
    }
}
