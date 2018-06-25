using DataLayer.Entities;
using DataLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using WebApplication1.Models;

namespace WebApplication1.Controllers.api
{
    [Authorize]
    public class StudentController : ApiController
    {
        private readonly IGenericRepository<Student, Guid> _studentRepository;
        private readonly IGenericRepository<User, Guid> _userRepository;
        private readonly IGenericRepository<Timetable, Guid> _timetableRepository;
        private readonly IGenericRepository<TeacherCourse, Guid> _courseRepository;
        private readonly IGenericRepository<Question, Guid> _questionRepository;
        private readonly IGenericRepository<Teacher, Guid> _teacherRepository;
        private readonly IGenericRepository<Answer, Guid> _answerRepository;

        public StudentController
            (
            IGenericRepository<Student, Guid> studentRepository,
            IGenericRepository<User, Guid> userRepository,
            IGenericRepository<Timetable, Guid> timetableRepository,
            IGenericRepository<TeacherCourse, Guid> courseRepository,
            IGenericRepository<Question, Guid> questionRepository,
            IGenericRepository<Teacher, Guid> teacherRepository,
            IGenericRepository<Answer, Guid> answerRepository
            )
        {
            _studentRepository = studentRepository;
            _userRepository = userRepository;
            _timetableRepository = timetableRepository;
            _courseRepository = courseRepository;
            _questionRepository = questionRepository;
            _teacherRepository = teacherRepository;
            _answerRepository = answerRepository;
        }

        [Route("/{username}")]
        public StudentModel GetStudentProfile(string username)
        {
            var user = _userRepository.GetAll().FirstOrDefault(x => x.Username == username);
            var student = _studentRepository.GetAll().FirstOrDefault(x => x.User == user);
            StudentModel studentModel = new StudentModel(student);
            return studentModel;
        }

        [HttpPost]
        public HttpStatusCode EditStudentProfile([FromBody]StudentModel studentModel)
        {
            var user = _userRepository.GetAll().FirstOrDefault(x => x.Username == studentModel.Username);
            var student = _studentRepository.GetAll().FirstOrDefault(x => x.User == user);

            student.FullName = studentModel.FullName;
            student.Year = studentModel.Year;
            student.Group = studentModel.Group;
            _studentRepository.Edit(student);
            _studentRepository.Save();
            return HttpStatusCode.OK;
        }

        [HttpPost]
        public HttpStatusCode RegisterStudent([FromBody]StudentModel studentModel)
        {
            var user = _userRepository.GetAll().Where(x => x.Username == studentModel.Username).FirstOrDefault();
            Student student = new Student
            {
                Id = Guid.NewGuid(),
                FullName = studentModel.FullName,
                Year = studentModel.Year,
                Group = studentModel.Group,
                User = user
            };
            _studentRepository.Add(student);
            _studentRepository.Save();
            return HttpStatusCode.Created;
        }

        [Route("/{username}")]
        public StudentTimetableModel GetStudentTimetable(string username)
        {
            var user = _userRepository.GetAll().Where(x => x.Username == username).FirstOrDefault();
            var student = _studentRepository.GetAll().FirstOrDefault(x => x.User == user);
            List<Timetable> studentTimetable = _timetableRepository.GetAll()
                .Where(x => (x.Group == ("I" + student.Year) || x.Group == ("I" + student.Year + student.Group[0]))).ToList();
            StudentTimetableModel studentTimetableModel = new StudentTimetableModel(studentTimetable);
            return studentTimetableModel;
        }

        [Route("/{username}")]
        public List<StudentStatusTimetableModel> GetStatusTimetable(string username)
        {
            var user = _userRepository.GetAll().Where(x => x.Username == username).FirstOrDefault();
            var student = _studentRepository.GetAll().FirstOrDefault(x => x.User == user);
            var answers = _answerRepository.GetAll().Include(x => x.Question).ThenInclude(x => x.Course).ThenInclude(x => x.Timetable).ToList();
            var studentTimetable = _timetableRepository.GetAll()
                .Where(x => (x.Group == ("I" + student.Year) || x.Group == ("I" + student.Year + student.Group[0])));
            List<StudentStatusTimetableModel> studentStatusTimetables = studentTimetable
                .Select(x => new StudentStatusTimetableModel(x, student, answers)).ToList();
            return studentStatusTimetables;
        }

        [Route("/{timetableId}/{username}")]
        public List<StudentStatusCourseModel> GetStatusCourse(string timetableId, string username)
        {
            var user = _userRepository.GetAll().FirstOrDefault(x => x.Username == username);
            var student = _studentRepository.GetAll().Include(x => x.User).FirstOrDefault(x => x.User == user);
            var timetable = _timetableRepository.GetAll().FirstOrDefault(x => x.Id == Guid.Parse(timetableId));
            var answers = _answerRepository.GetAll().Include(x => x.Question).ThenInclude(x => x.Course).ToList();
            List<StudentStatusCourseModel> studentStatusCourses = _courseRepository.GetAll()
                .Where(x => x.Timetable == timetable).Select(x => new StudentStatusCourseModel(x, student, answers)).ToList();
            return studentStatusCourses;
        }

        [Route("/{username}/{courseId}")]
        public List<StudentStatusModel> GetStudentStatus(string username, string courseId)
        {
            var user = _userRepository.GetAll().Where(x => x.Username == username).FirstOrDefault();
            var student = _studentRepository.GetAll().Include(x => x.User).FirstOrDefault(x => x.User == user);
            var course = _courseRepository.GetAll().FirstOrDefault(x => x.Id == Guid.Parse(courseId));
            var result = _answerRepository.GetAll()
                .Include(x => x.Question).ThenInclude(x => x.Course).ThenInclude(x => x.Teacher)
                .Include(x => x.Question).ThenInclude(x => x.Course)
                .Include(x => x.Question)
                .Where(x => x.Student == student && x.Question.Course == course)
                .Select(x => new StudentStatusModel(x))
                .ToList();
            return result;
        }
    }
}
