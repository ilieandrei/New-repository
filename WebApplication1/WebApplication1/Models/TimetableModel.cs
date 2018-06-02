using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class TimetableModel
    {
        public Guid Id { get; set; }
        public string Day { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Group { get; set; }
        public string Name { get; set; }
        public string Teacher { get; set; }
        public string Hall { get; set; }
        public string Week { get; set; }
        public string Pack { get; set; }

        public TimetableModel(Timetable timetable)
        {
            Id = timetable.Id;
            Day = timetable.Day;
            From = timetable.From;
            To = timetable.To;
            Group = timetable.Group;
            Name = timetable.Name;
            Teacher = timetable.Teacher;
            Hall = timetable.Hall;
            Week = timetable.Week;
            Pack = timetable.Pack;
        }

        public TimetableModel()
        { }
    }

    public class StudentTimetable
    {
        public Guid Id { get; set; }
        public string Day { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Name { get; set; }
        public string Teacher { get; set; }
        public string Hall { get; set; }
        public string Week { get; set; }

        public StudentTimetable()
        { }
    }

    public class StudentTimetableModel
    {
        public List<StudentTimetable> Timetables { get; set; }

        public StudentTimetableModel(List<Timetable> timetables)
        {
            Timetables = new List<StudentTimetable>();
            foreach (var item in timetables)
            {
                Timetables.Add(new StudentTimetable
                {
                    Id = item.Id,
                    Day = item.Day,
                    From = item.From,
                    To = item.To,
                    Name = item.Name,
                    Teacher = item.Teacher,
                    Hall = item.Hall,
                    Week = item.Week,
                });
            }
        }

        public StudentTimetableModel()
        { }
    }

    public class TeacherTimetable
    {
        public Guid Id { get; set; }
        public string Day { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Group { get; set; }
        public string Name { get; set; }
        public string Hall { get; set; }
        public string Week { get; set; }

        public TeacherTimetable()
        { }
    }

    public class TeacherTimetableModel
    {
        public List<TeacherTimetable> Timetables { get; set; }

        public TeacherTimetableModel(List<Timetable> timetables)
        {
            Timetables = new List<TeacherTimetable>();
            foreach (var item in timetables)
            {
                Timetables.Add(new TeacherTimetable
                {
                    Id = item.Id,
                    Day = item.Day,
                    From = item.From,
                    To = item.To,
                    Name = item.Name,
                    Hall = item.Hall,
                    Week = item.Week,
                    Group = item.Group
                });
            }
        }

        public TeacherTimetableModel()
        { }
    }

    public class AdminTimetableModel
    {
        public List<TimetableModel> timetableModels { get; set; }

        public AdminTimetableModel(List<Timetable> timetables)
        {
            timetableModels = new List<TimetableModel>();
            foreach (var item in timetables)
            {
                timetableModels.Add(new TimetableModel
                {
                    Id = item.Id,
                    Day = item.Day,
                    From = item.From,
                    Group = item.Group,
                    Hall = item.Hall,
                    Name = item.Name,
                    Pack = item.Pack,
                    Teacher = item.Teacher,
                    To = item.To,
                    Week = item.Week
                });
            }
        }

        public AdminTimetableModel()
        { }
    }
}
