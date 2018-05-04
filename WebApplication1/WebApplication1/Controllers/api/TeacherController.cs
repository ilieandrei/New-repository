using DataLayer.Entities;
using DataLayer.Repositories;
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
            teacher.Email = teacherModel.Email;
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
                Email = teacherModel.Email,
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
    }
}
