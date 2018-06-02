using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class TeacherStatusModel
    {
        public string Question { get; set; }
        public string Student { get; set; }
        public string Answer { get; set; }
        public double Rating { get; set; }

        public TeacherStatusModel(Answer answer)
        {
            Question = answer.Question.QuestionName;
            Student = answer.Student.FullName;
            Answer = answer.AnswerName;
            Rating = answer.Rating;
        }

        public TeacherStatusModel()
        { }
    }

    public class TeacherStatusTimetableModel
    {
        public Guid Id { get; set; }
        public string Timetable { get; set; }
        public string Group { get; set; }

        public TeacherStatusTimetableModel(Timetable timetable)
        {
            Id = timetable.Id;
            Timetable = timetable.Name;
            Group = timetable.Group;
        }

        public TeacherStatusTimetableModel()
        { }
    }

    public class TeacherStatusCourseModel
    {
        public Guid Id { get; set; }
        public string Course { get; set; }

        public TeacherStatusCourseModel(TeacherCourse course)
        {
            Id = course.Id;
            Course = course.Title;
        }

        public TeacherStatusCourseModel()
        { }
    }

    public class TeacherStatusStudentsModel
    {
        public string Student { get; set; }
        public double Points { get; set; }
        public int TotalAnswers { get; set; }

        public TeacherStatusStudentsModel(Student student, List<Answer> answers, Timetable timetable)
        {
            Student = student.FullName;
            Points = answers.Where(x => x.Student == student && x.Question.Course.Timetable == timetable).Sum(x => x.Rating);
            TotalAnswers = answers.Where(x => x.Student == student && x.Question.Course.Timetable == timetable).Count();
        }
    }
}
