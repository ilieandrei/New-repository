using DataLayer.Entities;
using DataLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using WebApplication1.Models;

namespace WebApplication1.Controllers.api
{
    public class TeacherController : ApiController
    {
        private readonly IGenericRepository<Teacher, Guid> _teacherRepository;
        private readonly IGenericRepository<User, Guid> _userRepository;
        private readonly IGenericRepository<Timetable, Guid> _timetableRepository;
        private readonly IGenericRepository<Student, Guid> _studentRepository;
        private readonly IGenericRepository<Answer, Guid> _answerRepository;
        private readonly IGenericRepository<TeacherCourse, Guid> _courseRepository;

        public TeacherController
            (
            IGenericRepository<Teacher, Guid> teacherRepository,
            IGenericRepository<User, Guid> userRepository,
            IGenericRepository<Timetable, Guid> timetableRepository,
            IGenericRepository<Student, Guid> studentRepository,
            IGenericRepository<Answer, Guid> answerRepository,
            IGenericRepository<TeacherCourse, Guid> courseRepository
            )
        {
            _teacherRepository = teacherRepository;
            _userRepository = userRepository;
            _timetableRepository = timetableRepository;
            _studentRepository = studentRepository;
            _answerRepository = answerRepository;
            _courseRepository = courseRepository;
        }

        [Route("/{username}")]
        public TeacherModel GetTeacherProfile(string username)
        {
            var user = _userRepository.GetAll().FirstOrDefault(x => x.Username == username);
            var teacher = _teacherRepository.GetAll().FirstOrDefault(x => x.User == user);
            TeacherModel teacherModel = new TeacherModel(teacher);
            return teacherModel;
        }

        [HttpPost]
        public HttpStatusCode EditTeacherProfile([FromBody]TeacherModel teacherModel)
        {
            var user = _userRepository.GetAll().FirstOrDefault(x => x.Username == teacherModel.Username);
            var teacher = _teacherRepository.GetAll().FirstOrDefault(x => x.User == user);

            teacher.FullName = teacherModel.FullName;
            teacher.Function = teacherModel.Function;
            _teacherRepository.Edit(teacher);
            _teacherRepository.Save();
            return HttpStatusCode.OK;
        }

        [HttpPost]
        public HttpStatusCode RegisterTeacher([FromBody]TeacherModel teacherModel)
        {
            var user = _userRepository.GetAll().Where(x => x.Username == teacherModel.Username).FirstOrDefault();
            Teacher teacher = new Teacher
            {
                Id = Guid.NewGuid(),
                FullName = teacherModel.FullName,
                Function = teacherModel.Function,
                User = user
            };
            _teacherRepository.Add(teacher);
            _teacherRepository.Save();
            return HttpStatusCode.Created;
        }

        [Route("/{username}")]
        public TeacherTimetableModel GetTeacherTimetable(string username)
        {
            var user = _userRepository.GetAll().Where(x => x.Username == username).FirstOrDefault();
            var teacher = _teacherRepository.GetAll().FirstOrDefault(x => x.User == user);
            List<Timetable> teacherTimetable = _timetableRepository.GetAll()
                .Where(x => x.Teacher.Contains(teacher.FullName)).ToList();
            TeacherTimetableModel teacherTimetableModel = new TeacherTimetableModel(teacherTimetable);
            return teacherTimetableModel;
        }

        [Route("/{username}")]
        public List<TeacherStatusTimetableModel> GetStatusTimetable(string username)
        {
            var user = _userRepository.GetAll().Where(x => x.Username == username).FirstOrDefault();
            var teacher = _teacherRepository.GetAll().FirstOrDefault(x => x.User == user);
            List<Timetable> teacherTimetable = _timetableRepository.GetAll()
                .Where(x => x.Teacher.Contains(teacher.FullName)).ToList();
            List<TeacherStatusTimetableModel> teacherStatusTimetables = teacherTimetable
                .Select(x => new TeacherStatusTimetableModel(x)).ToList();
            return teacherStatusTimetables;
        }

        [Route("/{timetableId}")]
        public List<TeacherStatusCourseModel> GetStatusCourse(string timetableId)
        {
            var timetable = _timetableRepository.GetAll().FirstOrDefault(x => x.Id == Guid.Parse(timetableId));
            List<TeacherStatusCourseModel> teacherStatusCourses = _courseRepository.GetAll()
                .Where(x => x.Timetable == timetable).Select(x => new TeacherStatusCourseModel(x)).ToList();
            return teacherStatusCourses;
        }

        [Route("/{username}/{courseId}")]
        public List<TeacherStatusModel> GetTeacherStatus(string username, string courseId)
        {
            var user = _userRepository.GetAll().Where(x => x.Username == username).FirstOrDefault();
            var course = _courseRepository.GetAll().FirstOrDefault(x => x.Id == Guid.Parse(courseId));
            var result = _answerRepository.GetAll()
                .Include(x => x.Question).ThenInclude(x => x.Course)
                .Include(x => x.Question).Include(x => x.Student)
                .Where(x => x.Question.Course == course)
                .Select(x => new TeacherStatusModel(x))
                .ToList();
            return result;
        }

        [Route("/{timetableId}")]
        public List<TeacherStatusStudentsModel> GetStudentsStatus(string timetableId)
        {
            var timetable = _timetableRepository.GetAll().FirstOrDefault(x => x.Id == Guid.Parse(timetableId));
            var answers = _answerRepository.GetAll().Include(x => x.Question).ThenInclude(x => x.Course).ThenInclude(x => x.Timetable).ToList();
            var students = _studentRepository.GetAll().Where(x => timetable.Group.Contains(x.Year) && timetable.Group.Contains(x.Group[0])).ToList();
            List<TeacherStatusStudentsModel> teacherStatusStudents = new List<TeacherStatusStudentsModel>();
            foreach (var item in students)
                teacherStatusStudents.Add(new TeacherStatusStudentsModel(item, answers, timetable));
            return teacherStatusStudents;
        }
    }
}
