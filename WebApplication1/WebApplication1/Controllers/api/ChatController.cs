using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using DataLayer.Entities;
using DataLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

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
        public HttpStatusCode PostMessage([FromBody]NewMessageModel chatModel)
        {
            var user = _userRepository.GetAll().FirstOrDefault(x => x.Username == chatModel.Username);
            var timetable = _timetableRepository.GetAll().FirstOrDefault(x => x.Id == Guid.Parse(chatModel.TimetableId));
            _chatRepository.Add(new Chat
            {
                User = user,
                Timetable = timetable,
                Message = chatModel.Message,
                PostTime = DateTime.Now
            });
            _chatRepository.Save();
            return HttpStatusCode.OK;
        }
        
        public ChatMessagesModel GetMessages([FromBody]MessageQueryModel messageQueryModel)
        {
            try
            {
                var timetable = _timetableRepository.GetAll().FirstOrDefault(x => x.Id == Guid.Parse(messageQueryModel.TimetableId));
                messageQueryModel.LastMessageTime = messageQueryModel.LastMessageTime.AddMilliseconds(1);
                var messages = _chatRepository.GetAll().Where(x => x.PostTime > messageQueryModel.LastMessageTime && x.Timetable == timetable)
                    .Include(y => y.User).ToList();
                if (messageQueryModel.IsEmpty)
                    messages = _chatRepository.GetAll().Where(x => x.Timetable == timetable)
                        .Include(y => y.User).ToList();
                ChatMessagesModel chatMessagesModel = new ChatMessagesModel(messages);
                return chatMessagesModel;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}
