using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Entities
{
    public class Chat : Entity<Guid>
    {
        public User User { get; set; }
        public Timetable Timetable { get; set; }
        public string Message { get; set; }
        public DateTime PostTime { get; set; }
    }
}
