using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class AdminUsersModel
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
        public bool IsBlocked { get; set; }

        public AdminUsersModel(User user, List<Student> students, List<Teacher> teachers)
        {
            Id = user.Id;
            Username = user.Username;
            FullName = user.Role == "Student" 
                ? students.FirstOrDefault(x => x.User == user).FullName 
                : teachers.FirstOrDefault(x => x.User == user).FullName;
            Role = user.Role;
            IsBlocked = user.IsBlocked;
        }

        public AdminUsersModel()
        { }
    }
}
