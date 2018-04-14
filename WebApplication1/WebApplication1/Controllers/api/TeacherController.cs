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
    public class TeacherController : ApiController
    {
        private readonly IGenericRepository<Teacher, Guid> _teacherRepository;
        private readonly IGenericRepository<User, Guid> _userRepository;
        private readonly IGenericRepository<Timetable, Guid> _timetableRepository;
        public TeacherController
            (
            IGenericRepository<Teacher, Guid> teacherRepository,
            IGenericRepository<User, Guid> userRepository,
            IGenericRepository<Timetable, Guid> timetableRepository
            )
        {
            _teacherRepository = teacherRepository;
            _userRepository = userRepository;
            _timetableRepository = timetableRepository;
        }

        [Route("/{username}")]
        public Teacher GetTeacherProfile(string username)
        {
            var user = _userRepository.GetAll().FirstOrDefault(x => x.Username == username);
            var teacher = _teacherRepository.GetAll().FirstOrDefault(x => x.User == user);
            if (teacher != null)
                return teacher;
            return null;
        }

        [HttpPost]
        [Route("/{username}/{teacherFullName}/{teacherTitle}/{teacherMail}")]
        public HttpStatusCode EditTeacherProfile(string username, string teacherFullName, string teacherTitle, string teacherMail)
        {
            var user = _userRepository.GetAll().FirstOrDefault(x => x.Username == username);
            var teacher = _teacherRepository.GetAll().FirstOrDefault(x => x.User == user);
            if (teacher != null)
            {
                teacher.FullName = teacherFullName;
                teacher.Function = teacherTitle;
                teacher.Email = teacherMail;
                _teacherRepository.Edit(teacher);
                _teacherRepository.Save();
                return HttpStatusCode.OK;
            }
            return HttpStatusCode.NotFound;
        }

        //Get teacher timetable
        [Route("/{username}")]
        public List<Timetable> GetTeacherTimetable(string username)
        {
            var user = _userRepository.GetAll().Where(x => x.Username == username).FirstOrDefault();
            var teacher = _teacherRepository.GetAll().FirstOrDefault(x => x.User == user);
            List<Timetable> teacherTimetable = _timetableRepository.GetAll()
                .Where(x => x.Teacher.Contains(teacher.FullName)).ToList();
            return teacherTimetable;
        }
    }
}
