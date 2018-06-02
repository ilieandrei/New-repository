using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Entities
{
    public class Student : Entity<Guid>
    {
        public string FullName { get; set; }
        public string Year { get; set; }
        public string Group { get; set; }
        public User User { get; set; }
    }
}
