using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    public class TeacherController : Controller
    {
        public IActionResult TeacherProfile()
        {
            return View();
        }

        public IActionResult TeacherProfileSettings()
        {
            return View();
        }

        public IActionResult TeacherTimetable()
        {
            return View();
        }

        public IActionResult TeacherStatus()
        {
            return View();
        }

        public IActionResult TeacherFullTimetable()
        {
            return View();
        }

        public IActionResult TeacherCourses()
        {
            return View();
        }

        public IActionResult TeacherAnswers()
        {
            return View();
        }

        public IActionResult StudentsStatus()
        {
            return View();
        }
    }
}
