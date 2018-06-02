using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    public class AdministratorController : Controller
    {
        public IActionResult AdminTimetable()
        {
            return View();
        }

        public IActionResult AdminUsers()
        {
            return View();
        }
    }
}
