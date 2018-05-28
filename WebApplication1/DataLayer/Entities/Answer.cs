using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Entities
{
    public class Answer:Entity<Guid>
    {
        public Question Question { get; set; }
        public Student Student { get; set; }
        public string AnswerName { get; set; }
        public DateTime AnswerTime { get; set; }
        public double Rating { get; set; }
    }
}
