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
        public TeacherController(IGenericRepository<Teacher, Guid> teacherRepository, IGenericRepository<User, Guid> userRepository)
        {
            _teacherRepository = teacherRepository;
            _userRepository = userRepository;
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
    }
}
