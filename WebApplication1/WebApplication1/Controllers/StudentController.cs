using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult StudentProfile()
        {
            return View();
        }

        public IActionResult StudentProfileSettings()
        {
            return View();
        }

        public IActionResult StudentTimetable()
        {
            return View();
        }

        public IActionResult StudentFullTimetable()
        {
            return View();
        }

        public IActionResult StudentAnswer()
        {
            return View();
        }

        public IActionResult StudentCourses()
        {
            return View();
        }

        public IActionResult StudentStatus()
        {
            return View();
        }
    }
}
