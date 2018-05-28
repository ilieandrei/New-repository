using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Entities
{
    public class Question : Entity<Guid>
    {
        public TeacherCourse Course { get; set; }
        public string QuestionName { get; set; }
        public int AnswerTime { get; set; }
        public bool IsLaunched { get; set; }
        public DateTime LaunchTime { get; set; }
    }
}
