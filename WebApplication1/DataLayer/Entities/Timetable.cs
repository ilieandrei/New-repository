using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Entities
{
    public class Timetable : Entity<Guid>
    {
        public string Day { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Group { get; set; }
        public string Name { get; set; }
        public string Teacher { get; set; }
        public string Hall { get; set; }
        public string Week { get; set; }
        public string Pack { get; set; }
    }
}
