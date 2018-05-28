using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class QuestionModel
    {
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        public string QuestionName { get; set; }
        public int AnswerTime { get; set; }
        public bool IsLaunched { get; set; }
        public int AnswersNumber { get; set; }

        public QuestionModel(Question question, List<Answer> answers)
        {
            Id = question.Id;
            CourseId = question.Course.Id;
            QuestionName = question.QuestionName;
            AnswerTime = question.AnswerTime;
            IsLaunched = question.IsLaunched;
            AnswersNumber = answers.Where(x => x.Question == question).Count();
        }

        public QuestionModel()
        { }
    }

    public class TeacherQuestionModel
    {
        public string CourseId { get; set; }
        public string Id { get; set; }
        public string QuestionName { get; set; }
        public int AnswerTime { get; set; }

        public TeacherQuestionModel()
        { }
    }

    public class StudentQuestionModel
    {
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        public string QuestionName { get; set; }
        public int AnswerTime { get; set; }
        public DateTime LaunchTime { get; set; }

        public StudentQuestionModel()
        { }
    }
}
