using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class TeacherModel
    {
        public string Function { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }

        public TeacherModel(Teacher teacher)
        {
            Function = teacher.Function;
            FullName = teacher.FullName;
            Username = teacher.User.Username;
        }

        public TeacherModel()
        { }
    }
}
