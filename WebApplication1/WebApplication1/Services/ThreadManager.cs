using DataLayer.Entities;
using DataLayer.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace WebApplication1.Services
{
    public class ThreadManager
    {
        private Dictionary<Timetable, Thread> _threadCollection;
        private List<Timetable> _timetables;
        private static void OnTimedEvent(object source, List<Timetable> timetables, Dictionary<Timetable, Thread> threads)
        {
            foreach(var item in timetables)
            {
                string fromHour = item.From;
                string toHour = item.To;
                string format = "hh:mm";
                CultureInfo provider = CultureInfo.InvariantCulture;
                //if (DateTime.Now.Hour== DateTime.ParseExact(fromHour, format, provider).Hour
                //    && DateTime.Now.Minute== DateTime.ParseExact(fromHour, format, provider).Minute
                //    && DateTime.Now.Second==0)
                //{
                //    threads.Add(item, new Thread())
                //}
            }
            if (DateTime.Now.Minute == 51 && DateTime.Now.Minute != 52)
            {

                ILoggerFactory loggerFactory1 = new LoggerFactory()
        .AddConsole()
        .AddDebug();
                ILogger logger = loggerFactory1.CreateLogger<Program>();
                logger.LogInformation(
                  "This is a test of the emergency broadcast system.");
            }
        }
        public ThreadManager()
        {
            GenericRepository<Timetable, Guid> genericRepository = new GenericRepository<Timetable, Guid>(new UnitOfWork(new DataLayer.ApplicationContext()));
            _timetables = genericRepository.GetAll().ToList();
            _threadCollection = new Dictionary<Timetable, Thread>();
            System.Timers.Timer aTimer = new System.Timers.Timer();
            //aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Elapsed += (sender, args) => OnTimedEvent(sender, _timetables, _threadCollection);
            aTimer.Interval = 2000;
            aTimer.Enabled = true;

            GC.KeepAlive(aTimer);
        }
    }
}
