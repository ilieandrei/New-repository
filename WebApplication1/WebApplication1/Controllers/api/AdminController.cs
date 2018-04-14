using DataLayer.Entities;
using DataLayer.Repositories;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApplication1.Controllers.api
{
    public class AdminController : ApiController
    {
        private readonly IGenericRepository<Timetable, Guid> _timetableRepository;

        public AdminController(IGenericRepository<Timetable, Guid> timetableRepository)
        {
            _timetableRepository = timetableRepository;
        }

        public string[,] getTimetableByYear(string url)
        {
            string html = string.Empty;
            //string url = "https://profs.info.uaic.ro/~orar/participanti/orar_I3.html";
            HtmlWeb web = new HtmlWeb();
            var doc = web.Load(url);
            var cnt = doc.DocumentNode.SelectNodes("//table[1]//tr").Count;
            string[,] timetable = new string[200, 10];
            int index = 0, timetableRow = 0, timetableCol = 0;
            string day = "Luni";
            for (var ind = 1; ind <= cnt; ind++)
                if (doc.DocumentNode.SelectNodes("//table[1]//tr[" + ind.ToString() + "]")
                    .FirstOrDefault(x => x.InnerText.Contains("Luni")) != null)
                {
                    index = ind;
                    break;
                }

            for (var i = index + 1; i <= cnt; i++)
            {
                var htmlRow = doc.DocumentNode.SelectNodes("//table[1]//tr[" + i.ToString() + "]");
                if (htmlRow.FirstOrDefault(x => x.InnerText.Contains("Marti")) != null)
                {
                    day = "Marti";
                }
                if (htmlRow.FirstOrDefault(x => x.InnerText.Contains("Miercuri")) != null)
                {
                    day = "Miercuri";
                }
                if (htmlRow.FirstOrDefault(x => x.InnerText.Contains("Joi")) != null)
                {
                    day = "Joi";
                }
                if (htmlRow.FirstOrDefault(x => x.InnerText.Contains("Vineri")) != null)
                {
                    day = "Vineri";
                }
                foreach (HtmlNode item in htmlRow)
                {
                    timetableCol = 0;
                    var cell = item.InnerText.Split("\n");
                    for (int j = 0; j < cell.Length; j++)
                    {
                        if (timetableCol == 0)
                        {
                            timetable[timetableRow, timetableCol] = day;
                            timetableCol++;
                        }
                        if (String.IsNullOrWhiteSpace(cell[j]))
                            continue;
                        cell[j] = cell[j].Trim();
                        string groups = "";
                        if (cell[j].Contains("I"))
                        {
                            int k = j;
                            while (cell[j].Contains("I1") || cell[j].Contains("I2") || cell[j].Contains("I3") ||
                                cell[j].Trim() == "" || cell[j].Trim().Contains(","))
                            {
                                groups += cell[j].Trim();
                                j++;
                            }
                        }
                        if (groups != "")
                        {
                            //Console.WriteLine("Grupe: " + groups);
                            timetable[timetableRow, timetableCol] = groups;
                            timetableCol++;
                        }
                        string teachers = "";
                        if (timetableCol == 6)
                        {
                            string[] digits = { "C0", "C1", "C2", "C3", "C4", "C5", "C6", "C7", "C8", "C9" };
                            while (!digits.Any(cell[j].Contains))
                            {
                                if (cell[j].Trim() != "")
                                    teachers += cell[j].Trim() + ";";
                                j++;
                            }
                        }
                        if (teachers != "")
                        {
                            //Console.WriteLine("Profesori: " + teachers);
                            timetable[timetableRow, timetableCol] = teachers;
                            timetableCol++;
                        }
                        if (cell[j].Contains(","))
                            while (cell[j].Contains("laptop") || cell[j].Contains("video") || cell[j].Trim() == "" || cell[j].Contains(","))
                                j++;
                        if (cell[j].Trim() == "&nbsp;")
                        {
                            //Console.WriteLine("<>");
                            timetable[timetableRow, timetableCol] = "";
                            timetableCol++;
                        }
                        else
                        {
                            //Console.WriteLine(cell[j].Trim());
                            timetable[timetableRow, timetableCol] = cell[j].Trim();
                            timetableCol++;
                        }
                    }
                    timetableRow++;
                }
                //Console.WriteLine("----------------------------------------------");
            }
            int timetableListCount = 0;
            for (int i = 0; i < timetableRow; i++)
                if (timetable[i, 5] == "Curs" || timetable[i, 6] == "&nbsp;;")
                    timetableListCount++;
            string[] timetableList = new string[timetableListCount];
            int timetableListIndex = 0;
            for (int i = 0; i < timetableRow; i++)
            {
                if (timetable[i, 5] != "Curs" || timetable[i, 6] == "&nbsp;;")
                    continue;
                for (int j = 0; j < timetableCol; j++)
                {
                    //Console.Write(timetable[i, j] + " | ");
                    timetableList[timetableListIndex] += timetable[i, j] + " | ";
                }
                timetableListIndex++;
                //Console.Write("\n");
            }
            return timetable;
        }

        public string GetTimetable()
        {
            var orar1 = getTimetableByYear("https://profs.info.uaic.ro/~orar/participanti/orar_I1.html");
            var orar2 = getTimetableByYear("https://profs.info.uaic.ro/~orar/participanti/orar_I2.html");
            var orar3 = getTimetableByYear("https://profs.info.uaic.ro/~orar/participanti/orar_I3.html");
            string result = "Orar an 1\n";
            var timetaleEntries = _timetableRepository.GetAll().ToList();
            if (timetaleEntries.Count != 0)
                foreach (var item in timetaleEntries)
                {
                    _timetableRepository.Delete(item);
                    _timetableRepository.Save();
                }
            for (int i = 0; i < orar1.GetLength(0); i++)
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
            }
            return result;
        }
    }
}
