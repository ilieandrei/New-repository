using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using DataLayer.Users;
using System.Linq;
using System.Threading.Tasks;
using System;
using DataLayer.Repositories;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Cryptography;
using System.Text;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        private readonly IGenericRepository<User, Guid> _userRepository;
        private readonly IGenericRepository<Student, Guid> _studentRepository;
        private readonly IGenericRepository<Teacher, Guid> _teacherRepository;

        //MD5 hash
        static string GetMd5Hash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult ProfileRegister()
        {
            return View();
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
                return View();
            return RedirectToAction("Login", "Account");
        }

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

        public IActionResult Administrator()
        {
            return View();
        }

        public AccountController(IGenericRepository<User, Guid> userRepository, IGenericRepository<Student, Guid> studentRepository, IGenericRepository<Teacher, Guid> teacherRepository)
        {
            _userRepository = userRepository;
            _studentRepository = studentRepository;
            _teacherRepository = teacherRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Register(string username, string password)
        {
            MD5 md5Hash = MD5.Create();
            string hashPassword = GetMd5Hash(md5Hash, password);
            var user = _userRepository.GetAll().FirstOrDefault(x => x.Username == username);
            if(user != null)
            {
                ModelState.AddModelError("RegisterError", "Utilizatorul există deja!");
                return View();
            }
            User newUser = new User
            {
                Id = new Guid(),
                Username = username,
                Password = hashPassword
            };
            _userRepository.Add(newUser);
            _userRepository.Save();
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username)
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            return RedirectToAction("ProfileRegister", "Account");
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            MD5 md5Hash = MD5.Create();
            string hashPassword = GetMd5Hash(md5Hash, password);
            var user = _userRepository.GetAll().FirstOrDefault(x => x.Username == username);
            if(user == null)
            {
                ModelState.AddModelError("LoginError", "Utilizatorul nu există!");
                return View();
            }
            if(user.Password != hashPassword)
            {
                ModelState.AddModelError("LoginError", "Parolă incorectă!");
                return View();
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username)
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            if (username == "admin")
                return RedirectToAction("Administrator", "Account");
            return RedirectToAction("Index", "Account");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public IActionResult StudentRegister(string username, string completeName, string email, string studyYear, string studentGroup)
        {
            var studentProfile = new Student()
            {
                Id = new Guid(),
                User = _userRepository.GetAll().FirstOrDefault(x => x.Username == username),
                FullName = completeName,
                Email = email,
                Year = studyYear,
                Group = studentGroup
            };
            var user = _userRepository.GetAll().FirstOrDefault(x => x.Username == username);
            if (user != null)
                user.Role = "Student";
            _studentRepository.Add(studentProfile);
            _userRepository.Edit(user);
            _studentRepository.Save();
            return RedirectToAction("Index", "Account");
        }

        [HttpPost]
        public IActionResult TeacherRegister(string username, string title, string completeName, string email)
        {
            var teacherProfile = new Teacher()
            {
                Id = new Guid(),
                User = _userRepository.GetAll().FirstOrDefault(x => x.Username == username),
                Function = title,
                FullName = completeName,
                Email = email
            };
            var user = _userRepository.GetAll().FirstOrDefault(x => x.Username == username);
            if (user != null)
                user.Role = "Teacher";
            _teacherRepository.Add(teacherProfile);
            _userRepository.Edit(user);
            _teacherRepository.Save();
            return RedirectToAction("Index", "Account");
        }
    }
}