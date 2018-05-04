using DataLayer.Entities;
using DataLayer.Repositories;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers.api
{
    public class AdminController : ApiController
    {
        private readonly IGenericRepository<Timetable, Guid> _timetableRepository;

        public AdminController(IGenericRepository<Timetable, Guid> timetableRepository)
        {
            _timetableRepository = timetableRepository;
        }

        public AdminTimetableModel GetTimetable()
        {
            /*var orar1 = getTimetableByYear("https://profs.info.uaic.ro/~orar/participanti/orar_I1.html");
            var orar2 = getTimetableByYear("https://profs.info.uaic.ro/~orar/participanti/orar_I2.html");
            var orar3 = getTimetableByYear("https://profs.info.uaic.ro/~orar/participanti/orar_I3.html");
            string result = "Orar an 1\n";*/
            /*for (int i = 0; i < orar1.GetLength(0); i++)
            {
                if (orar1[i, 5] != "Curs" || orar1[i, 6] == "&nbsp;;")
                    continue;
                Timetable timetable = new Timetable
                {
                    Id = new Guid(),
                    Day = orar1[i, 0],
                    From = orar1[i, 1],
                    To = orar1[i, 2],
                    Group = orar1[i, 3],
                    Name = orar1[i, 4],
                    Teacher = orar1[i, 6],
                    Hall = orar1[i, 7],
                    Week = orar1[i, 8],
                    Pack = orar1[i, 9]
                };
                _timetableRepository.Add(timetable);
                _timetableRepository.Save();
                result += timetable.Day + " | " + timetable.From + " | " + timetable.To + " | " + timetable.Group
                    + " | " + timetable.Name + " | " + timetable.Teacher + " | " + timetable.Hall + " | " + timetable.Week + " | "
                    + " | " + timetable.Pack + "\n";
            }
            for (int i = 0; i < orar2.GetLength(0); i++)
            {
                if (orar2[i, 5] != "Curs" || orar2[i, 6] == "&nbsp;;")
                    continue;
                Timetable timetable = new Timetable
                {
                    Id = new Guid(),
                    Day = orar2[i, 0],
                    From = orar2[i, 1],
                    To = orar2[i, 2],
                    Group = orar2[i, 3],
                    Name = orar2[i, 4],
                    Teacher = orar2[i, 6],
                    Hall = orar2[i, 7],
                    Week = orar2[i, 8],
                    Pack = orar2[i, 9]
                };
                _timetableRepository.Add(timetable);
                _timetableRepository.Save();
                result += timetable.Day + " | " + timetable.From + " | " + timetable.To + " | " + timetable.Group
                    + " | " + timetable.Name + " | " + timetable.Teacher + " | " + timetable.Hall + " | " + timetable.Week + " | "
                    + " | " + timetable.Pack + "\n";
            }
            for (int i = 0; i < orar3.GetLength(0); i++)
            {
                if (orar3[i, 5] != "Curs" || orar3[i, 6] == "&nbsp;;")
                    continue;
                Timetable timetable = new Timetable
                {
                    Id = new Guid(),
                    Day = orar3[i, 0],
                    From = orar3[i, 1],
                    To = orar3[i, 2],
                    Group = orar3[i, 3],
                    Name = orar3[i, 4],
                    Teacher = orar3[i, 6],
                    Hall = orar3[i, 7],
                    Week = orar3[i, 8],
                    Pack = orar3[i, 9]
                };
                _timetableRepository.Add(timetable);
                _timetableRepository.Save();
                result += timetable.Day + " | " + timetable.From + " | " + timetable.To + " | " + timetable.Group
                    + " | " + timetable.Name + " | " + timetable.Teacher + " | " + timetable.Hall + " | " + timetable.Week + " | "
                    + " | " + timetable.Pack + "\n";
            }*/
            var timetaleEntries = _timetableRepository.GetAll().ToList();
            if (timetaleEntries.Count != 0)
                foreach (var item in timetaleEntries)
                {
                    _timetableRepository.Delete(item);
                    _timetableRepository.Save();
                }
            TimetableService timetableService = new TimetableService();
            foreach(var item in timetableService.timetables)
            {
                _timetableRepository.Add(item);
                _timetableRepository.Save();
            }
            AdminTimetableModel adminTimetableModel = new AdminTimetableModel(timetableService.timetables);
            return adminTimetableModel;
        }
    }
}
