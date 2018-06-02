using DataLayer.Entities;
using DataLayer.Repositories;
using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers.api
{
    public class AdminController : ApiController
    {
        private readonly IGenericRepository<Timetable, Guid> _timetableRepository;
        private readonly IGenericRepository<TeacherCourse, Guid> _courseRepository;
        private readonly IGenericRepository<Question, Guid> _questionRepository;
        private readonly IGenericRepository<Answer, Guid> _answerRepository;
        private readonly IGenericRepository<User, Guid> _userRepository;
        private readonly IGenericRepository<Student, Guid> _studentRepository;
        private readonly IGenericRepository<Teacher, Guid> _teacherRepository;

        public AdminController
            (
            IGenericRepository<Timetable, Guid> timetableRepository,
            IGenericRepository<TeacherCourse, Guid> courseRepository,
            IGenericRepository<Question, Guid> questionRepository,
            IGenericRepository<Answer, Guid> answerRepository,
            IGenericRepository<User, Guid> userRepository,
            IGenericRepository<Student, Guid> studentRepository,
            IGenericRepository<Teacher, Guid> teacherRepository
            )
        {
            _timetableRepository = timetableRepository;
            _courseRepository = courseRepository;
            _questionRepository = questionRepository;
            _answerRepository = answerRepository;
            _userRepository = userRepository;
            _studentRepository = studentRepository;
            _teacherRepository = teacherRepository;
        }

        public AdminTimetableModel UpdateTimetable()
        {
            TimetableService timetableService = new TimetableService();
            foreach (var item in timetableService.timetables)
            {
                _timetableRepository.Add(item);
                _timetableRepository.Save();
            }
            AdminTimetableModel adminTimetableModel = new AdminTimetableModel(timetableService.timetables);
            return adminTimetableModel;
        }

        public AdminTimetableModel GetTimetable()
        {
            var timetable = _timetableRepository.GetAll().ToList();
            return new AdminTimetableModel(timetable);
        }

        public AdminTimetableModel RefreshTimetable()
        {
            var timetableEntries = _timetableRepository.GetAllNoTracking().ToList();
            TimetableService timetableService = new TimetableService();
            foreach (var item in timetableEntries)
            {
                var timetableToRefresh = timetableService.timetables.FirstOrDefault(x =>
                x.Group == item.Group && x.Name == item.Name && x.Teacher == item.Teacher);
                timetableToRefresh.Id = item.Id;
                _timetableRepository.Edit(timetableToRefresh);
                _timetableRepository.Save();
            }
            AdminTimetableModel adminTimetableModel = new AdminTimetableModel(timetableService.timetables);
            return adminTimetableModel;
        }

        [HttpPost]
        public HttpStatusCode DeleteAnswers()
        {
            var answers = _answerRepository.GetAll().ToList();
            foreach (var item in answers)
            {
                _answerRepository.Delete(item);
                _answerRepository.Save();
            }
            return HttpStatusCode.OK;
        }

        [HttpPost]
        public HttpStatusCode DeleteQuestions()
        {
            var questions = _questionRepository.GetAll().ToList();
            foreach (var item in questions)
            {
                _questionRepository.Delete(item);
                _questionRepository.Save();
            }
            return HttpStatusCode.OK;
        }

        [HttpPost]
        public HttpStatusCode DeleteCourses()
        {
            var courses = _courseRepository.GetAll().ToList();
            foreach (var item in courses)
            {
                _courseRepository.Delete(item);
                _courseRepository.Save();
            }
            return HttpStatusCode.OK;
        }

        [HttpPost]
        public HttpStatusCode DeleteTimetables()
        {
            var timetables = _timetableRepository.GetAll().ToList();
            foreach (var item in timetables)
            {
                _timetableRepository.Delete(item);
                _timetableRepository.Save();
            }
            return HttpStatusCode.OK;
        }

        public List<AdminUsersModel> GetUsers()
        {
            var students = _studentRepository.GetAll().Include(x => x.User).ToList();
            var teachers = _teacherRepository.GetAll().Include(x => x.User).ToList();
            return _userRepository.GetAll().Where(x => x.Role != "Administrator").Select(x => new AdminUsersModel(x, students, teachers)).ToList();
        }

        [HttpPost]
        [Route("/{username}")]
        public HttpStatusCode BlockUser(string username)
        {
            var user = _userRepository.GetAll().FirstOrDefault(x => x.Username == username);
            user.IsBlocked = true;
            _userRepository.Edit(user);
            _userRepository.Save();
            return HttpStatusCode.OK;
        }

        [HttpPost]
        [Route("/{username}")]
        public HttpStatusCode UnlockUser(string username)
        {
            var user = _userRepository.GetAll().FirstOrDefault(x => x.Username == username);
            user.IsBlocked = false;
            _userRepository.Edit(user);
            _userRepository.Save();
            return HttpStatusCode.OK;
        }
    }
}
