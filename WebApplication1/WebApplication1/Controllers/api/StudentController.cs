using DataLayer.Entities;
using DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using WebApplication1.Models;

namespace WebApplication1.Controllers.api
{
    public class StudentController : ApiController
    {
        private readonly IGenericRepository<Student, Guid> _studentRepository;
        private readonly IGenericRepository<User, Guid> _userRepository;
        private readonly IGenericRepository<Timetable, Guid> _timetableRepository;
        public StudentController
            (
            IGenericRepository<Student, Guid> studentRepository,
            IGenericRepository<User, Guid> userRepository,
            IGenericRepository<Timetable, Guid> timetableRepository
            )
        {
            _studentRepository = studentRepository;
            _userRepository = userRepository;
            _timetableRepository = timetableRepository;
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
            student.Email = studentModel.Email;
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
                Email = studentModel.Email,
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
    }
}
