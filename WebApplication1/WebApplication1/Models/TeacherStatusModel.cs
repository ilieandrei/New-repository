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

        public TeacherStatusTimetableModel(Timetable timetable)
        {
            Id = timetable.Id;
            Timetable = timetable.Name;
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
}
