using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Entities
{
    public class Teacher : Entity<Guid>
    {
        public string Email { get; set; }
        public string Function { get; set; }
        public string FullName { get; set; }
        public User User { get; set; }
    }
}
