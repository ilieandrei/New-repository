using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Entities
{
    public class TeacherCourse : Entity<Guid>
    {
        public Teacher Teacher { get; set; }
        public Timetable Timetable { get; set; }
        public string Title { get; set; }
    }
}
