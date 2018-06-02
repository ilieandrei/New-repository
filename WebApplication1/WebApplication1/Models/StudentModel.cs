using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class StudentModel
    {
        public string FullName { get; set; }
        public string Year { get; set; }
        public string Group { get; set; }
        public string Username { get; set; }

        public StudentModel(Student student)
        {
            FullName = student.FullName;
            Year = student.Year;
            Group = student.Group;
            Username = student.User.Username;
        }

        public StudentModel()
        { }
    }
}
