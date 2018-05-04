using DataLayer.Entities;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Services
{
    public class TimetableService
    {
        public List<Timetable> timetables;

        public string[,] GetTimetableByYear(string url)
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

        public TimetableService()
        {
            var timetable1 = GetTimetableByYear("https://profs.info.uaic.ro/~orar/participanti/orar_I1.html");
            var timetable2 = GetTimetableByYear("https://profs.info.uaic.ro/~orar/participanti/orar_I2.html");
            var timetable3 = GetTimetableByYear("https://profs.info.uaic.ro/~orar/participanti/orar_I3.html");
            for(int i=0;i<timetable1.GetLength(0);i++)
            {
                if (timetable1[i, 5] != "Curs" || timetable1[i, 6] == "&nbsp;;")
                    continue;
                timetables.Add(new Timetable
                {
                    Day = timetable1[i, 0],
                    From = timetable1[i, 1],
                    To = timetable1[i, 2],
                    Group = timetable1[i, 3],
                    Name = timetable1[i, 4],
                    Teacher = timetable1[i, 6],
                    Hall = timetable1[i, 7],
                    Week = timetable1[i, 8],
                    Pack = timetable1[i, 9]
                });
            }
            for (int i = 0; i < timetable2.GetLength(0); i++)
            {
                if (timetable2[i, 5] != "Curs" || timetable2[i, 6] == "&nbsp;;")
                    continue;
                timetables.Add(new Timetable
                {
                    Day = timetable2[i, 0],
                    From = timetable2[i, 1],
                    To = timetable2[i, 2],
                    Group = timetable2[i, 3],
                    Name = timetable2[i, 4],
                    Teacher = timetable2[i, 6],
                    Hall = timetable2[i, 7],
                    Week = timetable2[i, 8],
                    Pack = timetable2[i, 9]
                });
            }
            for (int i = 0; i < timetable3.GetLength(0); i++)
            {
                if (timetable3[i, 5] != "Curs" || timetable3[i, 6] == "&nbsp;;")
                    continue;
                timetables.Add(new Timetable
                {
                    Day = timetable3[i, 0],
                    From = timetable3[i, 1],
                    To = timetable3[i, 2],
                    Group = timetable3[i, 3],
                    Name = timetable3[i, 4],
                    Teacher = timetable3[i, 6],
                    Hall = timetable3[i, 7],
                    Week = timetable3[i, 8],
                    Pack = timetable3[i, 9]
                });
            }
        }
    }
}
