using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using WebApplication1.Controllers.api;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication1.Controllers
{
    [AllowAnonymous]
    public class RouteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Home()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult RegisterForm()
        {
            return View();
        }

        /*[HttpGet]
        public async Task<IActionResult> HomeSession(string username, string password)
        {
            AuthenticationController authenticationController = new AuthenticationController();
            if (authenticationController.LoginUser(username, password) == HttpStatusCode.Found)
            {
                var claims = new List<Claim> { new Claim(ClaimTypes.Name, username) };

                var userIdentity = new ClaimsIdentity(claims, "login");

                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(principal);
                
                return RedirectToAction("RegisterForm", "Route");
            }
            return Redirect("/#!/");
        }*/
        /*public IActionResult Home()
        {
            HttpContext.Session.SetString("Test", "Ben Rules!");
            return View();
        }
        public IActionResult RegisterForm()
        {
            ViewBag.Message = HttpContext.Session.GetString("Test");

            return View();
        }
        public void Login(string username, string password)
        {
            AuthenticationController authenticationController = new AuthenticationController();
            if(authenticationController.LoginUser(username, password) == HttpStatusCode.Found)
            {
                Guid token = Guid.NewGuid();
                HttpContext.Session.SetString(username, token.ToString());
            }
        }*/
    }
}
