using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class CourseModel
    {
        public Guid Id { get; set; }
        public Guid TimetableId { get; set; }
        public string Title { get; set; }
        public int QuestionsNumber { get; set; }

        public CourseModel(TeacherCourse teacherCourse, List<Question> questions)
        {
            Id = teacherCourse.Id;
            TimetableId = teacherCourse.Timetable.Id;
            Title = teacherCourse.Title;
            QuestionsNumber = questions.Where(x => x.Course == teacherCourse).Count();
        }

        public CourseModel()
        { }
    }

    public class TeacherCourseModel
    {
        public string Username { get; set; }
        public string TimetableId { get; set; }
        public string Title { get; set; }

        public TeacherCourseModel()
        { }
    }
}
