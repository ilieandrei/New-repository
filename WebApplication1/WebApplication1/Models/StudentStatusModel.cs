using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class StudentStatusModel
    {
        public string Question { get; set; }
        public string Teacher { get; set; }
        public string Answer { get; set; }
        public double Rating { get; set; }

        public StudentStatusModel(Answer answer)
        {
            Question = answer.Question.QuestionName;
            Teacher = answer.Question.Course.Teacher.FullName;
            Answer = answer.AnswerName;
            Rating = answer.Rating;
        }

        public StudentStatusModel()
        { }
    }

    public class StudentStatusTimetableModel
    {
        public Guid Id { get; set; }
        public string Timetable { get; set; }
        public double Points { get; set; }

        public StudentStatusTimetableModel(Timetable timetable, Student student, List<Answer> answers)
        {
            Id = timetable.Id;
            Timetable = timetable.Name;
            Points = answers.Where(x => x.Question.Course.Timetable == timetable && x.Student == student).Sum(x => x.Rating);
        }

        public StudentStatusTimetableModel()
        { }
    }

    public class StudentStatusCourseModel
    {
        public Guid Id { get; set; }
        public string Course { get; set; }
        public double Points { get; set; }

        public StudentStatusCourseModel(TeacherCourse course, Student student, List<Answer> answers)
        {
            Id = course.Id;
            Course = course.Title;
            Points = answers.Where(x => x.Question.Course == course && x.Student == student).Sum(x => x.Rating);
        }

        public StudentStatusCourseModel()
        { }
    }
}
