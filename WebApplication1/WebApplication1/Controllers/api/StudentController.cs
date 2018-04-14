using DataLayer.Entities;
using DataLayer.Repositories;
using DataLayer.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

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
        public Student GetStudentProfile(string username)
        {
            var user = _userRepository.GetAll().FirstOrDefault(x => x.Username == username);
            var student = _studentRepository.GetAll().FirstOrDefault(x => x.User == user);
            if (student != null)
                return student;
            return null;
        }

        [HttpPost]
        [Route("/{username}/{studentFullName}/{studyYear}/{studentGroup}/{studentMail}")]
        public HttpStatusCode EditStudentProfile(string username, string studentFullName, string studyYear, string studentGroup, string studentMail)
        {
            var user = _userRepository.GetAll().FirstOrDefault(x => x.Username == username);
            var student = _studentRepository.GetAll().FirstOrDefault(x => x.User == user);
            if (student != null)
            {
                student.FullName = studentFullName;
                student.Year = studyYear;
                student.Group = studentGroup;
                student.Email = studentMail;
                _studentRepository.Edit(student);
                _studentRepository.Save();
                return HttpStatusCode.OK;
            }
            return HttpStatusCode.NotFound;
        }

        //Register Student
        [HttpPost]
        [Route("/{fullname}/{email}/{year}/{group}/{username}")]
        public HttpStatusCode RegisterStudent(string fullname, string email, string year, string group, string username)
        {
            if (ModelState.IsValid)
            {
                var user = _userRepository.GetAll().Where(x => x.Username == username).FirstOrDefault();
                Student student = new Student
                {
                    Id = Guid.NewGuid(),
                    FullName = fullname,
                    Email = email,
                    Year = year,
                    Group = group,
                    User = user
                };
                _studentRepository.Add(student);
                _studentRepository.Save();
                return HttpStatusCode.Created;
            }
            return HttpStatusCode.NotFound;
        }

        //Get student timetable
        [Route("/{username}")]
        public List<Timetable> GetStudentTimetable(string username)
        {
            var user = _userRepository.GetAll().Where(x => x.Username == username).FirstOrDefault();
            var student = _studentRepository.GetAll().FirstOrDefault(x => x.User == user);
            List<Timetable> studentTimetable = _timetableRepository.GetAll()
                .Where(x => (x.Group == ("I" + student.Year) || x.Group == ("I" + student.Year + student.Group[0]))).ToList();
            return studentTimetable;
        }
    }
}
