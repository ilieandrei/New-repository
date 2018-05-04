using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class ChatModel
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public Guid TimetableId { get; set; }
        public string Message { get; set; }
        public DateTime PostTime { get; set; }

        public ChatModel(Chat chat)
        {
            Id = chat.Id;
            Username = chat.User.Username;
            TimetableId = chat.Timetable.Id;
            Message = chat.Message;
            PostTime = chat.PostTime;
        }

        public ChatModel()
        { }
    }

    public class NewMessageModel
    {
        public string Username { get; set; }
        public string TimetableId { get; set; }
        public string Message { get; set; }

        public NewMessageModel(Chat chat)
        {
            Username = chat.User.Username;
            TimetableId = chat.Timetable.Id.ToString();
            Message = chat.Message;
        }

        public NewMessageModel()
        { }
    }

    public class MessageQueryModel
    {
        public bool IsEmpty { get; set; }
        public DateTime LastMessageTime { get; set; }
        public string TimetableId { get; set; }

        public MessageQueryModel()
        { }
    }

    public class ChatMessagesModel
    {
        public List<ChatModel> chatModels;

        public ChatMessagesModel(List<Chat> chats)
        {
            chatModels = new List<ChatModel>();
            foreach(var item in chats)
            {
                chatModels.Add(new ChatModel
                {
                    Id=item.Id,
                    Message = item.Message,
                    PostTime = item.PostTime,
                    TimetableId = item.Timetable.Id,
                    Username = item.User.Username
                });
            }
        }

        public ChatMessagesModel()
        { }
    }
}
