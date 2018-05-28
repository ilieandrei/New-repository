using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class AnswerModel
    {
        public Guid Id { get; set; }
        public Guid QuestionId { get; set; }
        public string QuestionName { get; set; }
        public string StudentUsername { get; set; }
        public string StudentName { get; set; }
        public string AnswerName { get; set; }
        public DateTime AnswerTime { get; set; }
        public double Rating { get; set; }

        public AnswerModel(Answer answer)
        {
            Id = answer.Id;
            QuestionId = answer.Question.Id;
            QuestionName = answer.Question.QuestionName;
            StudentUsername = answer.Student.User.Username;
            StudentName = answer.Student.FullName;
            AnswerName = answer.AnswerName;
            AnswerTime = answer.AnswerTime;
            Rating = answer.Rating;
        }

        public AnswerModel()
        { }
    }

    public class StudentAnswerModel
    {
        public string QuestionId { get; set; }
        public string StudentUsername { get; set; }
        public string AnswerName { get; set; }

        public StudentAnswerModel()
        { }
    }
}
