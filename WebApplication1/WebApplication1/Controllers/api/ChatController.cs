using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using DataLayer.Entities;
using DataLayer.Repositories;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Controllers.api
{
    public class ChatController : ApiController
    {
        private readonly IGenericRepository<User, Guid> _userRepository;
        private readonly IGenericRepository<Student, Guid> _studentRepository;
        private readonly IGenericRepository<Teacher, Guid> _teacherRepository;
        private readonly IGenericRepository<Timetable, Guid> _timetableRepository;
        private readonly IGenericRepository<Chat, Guid> _chatRepository;

        public ChatController
            (
            IGenericRepository<User, Guid> userRepository,
            IGenericRepository<Student, Guid> studentRepository,
            IGenericRepository<Teacher, Guid> teacherRepository,
            IGenericRepository<Timetable, Guid> timetableRepository,
            IGenericRepository<Chat, Guid> chatRepository
            )
        {
            _userRepository = userRepository;
            _studentRepository = studentRepository;
            _teacherRepository = teacherRepository;
            _timetableRepository = timetableRepository;
            _chatRepository = chatRepository;
        }

        [HttpPost]
        [Route("/{username}/{timetableId}/{message}")]
        public HttpStatusCode PostMessage(string username, string timetableId, string message)
        {
            var user = _userRepository.GetAll().FirstOrDefault(x => x.Username == username);
            var timetable = _timetableRepository.GetAll().FirstOrDefault(x => x.Id == Guid.Parse(timetableId));
            _chatRepository.Add(new Chat
            {
                User = user,
                Timetable = timetable,
                Message = message,
                PostTime = DateTime.Now
            });
            _chatRepository.Save();
            return HttpStatusCode.OK;
        }

        [Route("/{isEmpty}/{lastMessageTime}/{timetableId}")]
        public List<Chat> GetMessages(bool isEmpty, DateTime lastMessageTime, Guid timetableId)
        {
            var timetable = _timetableRepository.GetAll().FirstOrDefault(x => x.Id == timetableId);
            if (timetable == null)
                return null;
            var xx = _chatRepository.GetAll().Where(x => x.Timetable == timetable).FirstOrDefault();
            if (xx == null)
                return null;
            lastMessageTime = lastMessageTime.AddMilliseconds(1);
            var messages = _chatRepository.GetAll().Where(x => x.PostTime > lastMessageTime && x.Timetable == timetable)
                .Include(y=>y.User).ToList();
            if (isEmpty)
                messages = _chatRepository.GetAll().Where(x => x.Timetable == timetable)
                    .Include(y=>y.User).ToList();
            return messages;
        }
    }
}
