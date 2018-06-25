using DataLayer.Entities;
using DataLayer.Repositories;
using Microsoft.AspNetCore.Http;
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
    [Authorize]
    public class QuestionAnswerController : ApiController
    {
        private readonly IGenericRepository<User, Guid> _userRepository;
        private readonly IGenericRepository<Teacher, Guid> _teacherRepository;
        private readonly IGenericRepository<Student, Guid> _studentRepository;
        private readonly IGenericRepository<Timetable, Guid> _timetableRepository;
        private readonly IGenericRepository<TeacherCourse, Guid> _courseRepository;
        private readonly IGenericRepository<Question, Guid> _questionRepository;
        private readonly IGenericRepository<Answer, Guid> _answerRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public QuestionAnswerController
            (
            IGenericRepository<User, Guid> userRepository,
            IGenericRepository<Teacher, Guid> teacherRepository,
            IGenericRepository<Student, Guid> studentRepository,
            IGenericRepository<Timetable, Guid> timetableRepository,
            IGenericRepository<TeacherCourse, Guid> courseRepository,
            IGenericRepository<Question, Guid> questionRepository,
            IGenericRepository<Answer, Guid> answerRepository,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _userRepository = userRepository;
            _teacherRepository = teacherRepository;
            _studentRepository = studentRepository;
            _timetableRepository = timetableRepository;
            _courseRepository = courseRepository;
            _questionRepository = questionRepository;
            _answerRepository = answerRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        [Route("/{username}/{timetableId}")]
        public List<CourseModel> GetTeacherCourses(string username, string timetableId)
        {
            var user = _userRepository.GetAll().FirstOrDefault(x => x.Username == username);
            var teacher = _teacherRepository.GetAll().FirstOrDefault(x => x.User == user);
            var timetable = _timetableRepository.GetAll().FirstOrDefault(x => x.Id == Guid.Parse(timetableId));
            var questions = _questionRepository.GetAll().ToList();
            List<CourseModel> courseModels = _courseRepository.GetAll().Include(x => x.Timetable)
                .Where(x => x.Teacher == teacher && x.Timetable == timetable)
                .Select(x => new CourseModel(x, questions)).ToList();
            return courseModels;
        }

        [Route("/{timetableId}/{teacherName}")]
        public List<CourseModel> GetStudentCourses(string timetableId, string teacherName)
        {
            var teacher = _teacherRepository.GetAll().FirstOrDefault(x => teacherName.Contains(x.FullName));
            if (teacher == null)
                return null;
            var timetable = _timetableRepository.GetAll().FirstOrDefault(x => x.Id == Guid.Parse(timetableId));
            var questions = _questionRepository.GetAll().ToList();
            List<CourseModel> courseModels = _courseRepository.GetAll().Include(x => x.Timetable).Include(x => x.Teacher)
                .Where(x => x.Teacher == teacher && x.Timetable == timetable)
                .Select(x => new CourseModel(x, questions)).ToList();
            return courseModels;
        }

        [HttpPost]
        public HttpStatusCode AddTeacherCourse([FromBody]TeacherCourseModel courseModel)
        {
            var user = _userRepository.GetAll().FirstOrDefault(x => x.Username == courseModel.Username);
            var teacher = _teacherRepository.GetAll().FirstOrDefault(x => x.User == user);
            var timetable = _timetableRepository.GetAll().FirstOrDefault(x => x.Id == Guid.Parse(courseModel.TimetableId));
            TeacherCourse teacherCourse = new TeacherCourse
            {
                Teacher = teacher,
                Timetable = timetable,
                Title = courseModel.Title
            };
            _courseRepository.Add(teacherCourse);
            _courseRepository.Save();
            return HttpStatusCode.OK;
        }

        [Route("/{courseId}")]
        public List<QuestionModel> GetQuestions(string courseId)
        {
            var course = _courseRepository.GetAll().FirstOrDefault(x => x.Id == Guid.Parse(courseId));
            var answers = _answerRepository.GetAll().ToList();
            List<QuestionModel> questionModels = _questionRepository.GetAll().Include(x => x.Course)
                .Where(x => x.Course == course)
                .Select(x => new QuestionModel(x, answers)).ToList();
            return questionModels;
        }

        [HttpPost]
        public HttpStatusCode AddQuestion([FromBody]TeacherQuestionModel questionModel)
        {
            var course = _courseRepository.GetAll().FirstOrDefault(x => x.Id == Guid.Parse(questionModel.CourseId));
            Question question = new Question
            {
                Course = course,
                QuestionName = questionModel.QuestionName,
                AnswerTime = questionModel.AnswerTime,
                IsLaunched = false
            };
            _questionRepository.Add(question);
            _questionRepository.Save();
            return HttpStatusCode.OK;
        }

        [HttpPost]
        [Route("/{questionId}")]
        public HttpStatusCode LaunchQuestion(string questionId)
        {
            var question = _questionRepository.GetAll().FirstOrDefault(x => x.Id == Guid.Parse(questionId));
            question.IsLaunched = true;
            question.LaunchTime = DateTime.Now;
            _questionRepository.Edit(question);
            _questionRepository.Save();
            return HttpStatusCode.OK;
        }

        [HttpPost]
        [Route("/{questionId}")]
        public HttpStatusCode StopTime(string questionId)
        {
            var question = _questionRepository.GetAll().FirstOrDefault(x => x.Id == Guid.Parse(questionId));
            question.IsLaunched = false;
            _questionRepository.Edit(question);
            _questionRepository.Save();
            return HttpStatusCode.OK;
        }

        [Route("/{courseId}")]
        public StudentQuestionModel GetQuestion(string courseId)
        {
            if (courseId == "No content")
                return null;
            var course = _courseRepository.GetAll().FirstOrDefault(x => x.Id == Guid.Parse(courseId));
            var questions = _questionRepository.GetAll().Where(x => x.Course == course);
            foreach (var question in questions)
            {
                if (question.IsLaunched == true)
                {
                    StudentQuestionModel questionModel = new StudentQuestionModel
                    {
                        Id = question.Id,
                        CourseId = question.Course.Id,
                        QuestionName = question.QuestionName,
                        AnswerTime = question.AnswerTime,
                        LaunchTime = question.LaunchTime
                    };
                    return questionModel;
                }
            }
            return null;
        }

        [Route("/{questionId}")]
        public List<AnswerModel> GetAnswers(string questionId)
        {
            var question = _questionRepository.GetAll().FirstOrDefault(x => x.Id == Guid.Parse(questionId));
            List<AnswerModel> answerModels = _answerRepository.GetAll().Where(x => x.Question == question)
                .Include(x => x.Question).Include(x => x.Student).ThenInclude(x => x.User)
                .Select(x => new AnswerModel(x)).ToList();
            return answerModels;
        }

        [HttpPost]
        public HttpStatusCode AddAnswer([FromBody]StudentAnswerModel answerModel)
        {
            var user = _userRepository.GetAll().FirstOrDefault(x => x.Username == answerModel.StudentUsername);
            var student = _studentRepository.GetAll().FirstOrDefault(x => x.User == user);
            var question = _questionRepository.GetAll().FirstOrDefault(x => x.Id == Guid.Parse(answerModel.QuestionId));
            Answer answer = new Answer
            {
                Student = student,
                Question = question,
                AnswerName = answerModel.AnswerName,
                AnswerTime = DateTime.Now
            };
            _answerRepository.Add(answer);
            _answerRepository.Save();
            return HttpStatusCode.OK;
        }

        [HttpPost]
        [Route("/{courseId}/{courseName}")]
        public HttpStatusCode EditCourse(string courseId, string courseName)
        {
            var course = _courseRepository.GetAll().FirstOrDefault(x => x.Id == Guid.Parse(courseId));
            course.Title = courseName;
            _courseRepository.Edit(course);
            _courseRepository.Save();
            return HttpStatusCode.OK;
        }

        [HttpPost]
        [Route("/{courseId}")]
        public HttpStatusCode DeleteCourse(string courseId)
        {
            var course = _courseRepository.GetAll().FirstOrDefault(x => x.Id == Guid.Parse(courseId));
            bool containsQuestions = _questionRepository.GetAll().Any(x => x.Course == course);
            if (!containsQuestions)
            {
                _courseRepository.Delete(course);
                _courseRepository.Save();
                return HttpStatusCode.OK;
            }
            return HttpStatusCode.NotFound;
        }

        [HttpPost]
        public HttpStatusCode EditQuestion([FromBody]TeacherQuestionModel questionModel)
        {
            var question = _questionRepository.GetAll().FirstOrDefault(x => x.Id == Guid.Parse(questionModel.Id));
            question.QuestionName = questionModel.QuestionName;
            question.AnswerTime = questionModel.AnswerTime;
            _questionRepository.Edit(question);
            _questionRepository.Save();
            return HttpStatusCode.OK;
        }

        [HttpPost]
        [Route("/{questionId}")]
        public HttpStatusCode DeleteQuestion(string questionId)
        {
            var question = _questionRepository.GetAll().FirstOrDefault(x => x.Id == Guid.Parse(questionId));
            bool containsAnswers = _answerRepository.GetAll().Any(x => x.Question == question);
            if (!containsAnswers)
            {
                _questionRepository.Delete(question);
                _questionRepository.Save();
                return HttpStatusCode.OK;
            }
            return HttpStatusCode.NotFound;
        }

        [HttpPost]
        [Route("/{answerId}")]
        public HttpStatusCode DeleteAnswer(string answerId)
        {
            var answer = _answerRepository.GetAll().FirstOrDefault(x => x.Id == Guid.Parse(answerId));
            _answerRepository.Delete(answer);
            _answerRepository.Save();
            return HttpStatusCode.OK;
        }

        [HttpPost]
        [Route("/{answerId}/{answerRate}")]
        public HttpStatusCode RateAnswer(string answerId, double answerRate)
        {
            var answer = _answerRepository.GetAll().FirstOrDefault(x => x.Id == Guid.Parse(answerId));
            answer.Rating = answerRate;
            _answerRepository.Edit(answer);
            _answerRepository.Save();
            return HttpStatusCode.OK;
        }

        [HttpPost]
        [Route("/{currentTimetableId}")]
        public void SetCurrentTimetableId(string currentTimetableId)
        {
            if (currentTimetableId == null)
                _httpContextAccessor.HttpContext.Session.SetString("CurrentTimetableId", "No content");
            else
                _httpContextAccessor.HttpContext.Session.SetString("CurrentTimetableId", currentTimetableId);
        }

        public string GetCurrentTimetableId()
        {
            if (_httpContextAccessor.HttpContext.Session.GetString("CurrentTimetableId") == null)
                return "No content";
            return _httpContextAccessor.HttpContext.Session.GetString("CurrentTimetableId");
        }

        [HttpPost]
        [Route("/{currentCourseId}")]
        public void SetCurrentCourseId(string currentCourseId)
        {
            if (currentCourseId == null)
                _httpContextAccessor.HttpContext.Session.SetString("CurrentCourseId", "No content");
            else
                _httpContextAccessor.HttpContext.Session.SetString("CurrentCourseId", currentCourseId);
        }

        public string GetCurrentCourseId()
        {
            if (_httpContextAccessor.HttpContext.Session.GetString("CurrentCourseId") == null)
                return "No content";
            return _httpContextAccessor.HttpContext.Session.GetString("CurrentCourseId");
        }

        [HttpPost]
        [Route("/{currentTimetableId}/{currentTeacherName}")]
        public void SetCurrentStudentTimetable(string currentTimetableId, string currentTeacherName)
        {
            if (currentTeacherName == null && currentTimetableId == null)
            {
                _httpContextAccessor.HttpContext.Session.SetString("CurrentTimetableId", "No content");
                _httpContextAccessor.HttpContext.Session.SetString("CurrentTeacherName", "No content");
            }
            else
            {
                _httpContextAccessor.HttpContext.Session.SetString("CurrentTimetableId", currentTimetableId);
                _httpContextAccessor.HttpContext.Session.SetString("CurrentTeacherName", currentTeacherName);
            }
        }

        public string[] GetCurrentStudentTimetable()
        {
            string[] str = {
                _httpContextAccessor.HttpContext.Session.GetString("CurrentTimetableId"),
                _httpContextAccessor.HttpContext.Session.GetString("CurrentTeacherName")
            };
            if (str[0] == null || str[1] == null)
                return new string[] { "No content", "No content" };
            return str;
        }
    }
}
