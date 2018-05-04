using DataLayer.Entities;
using DataLayer.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using WebApplication1.Models;

namespace WebApplication1.Controllers.api
{
    public class UserController : ApiController
    {
        private readonly IGenericRepository<User, Guid> _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserController(IGenericRepository<User, Guid> userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        [Route("/{username}")]
        public string GetUserRole(string username)
        {
            var user = _userRepository.GetAll().FirstOrDefault(x => x.Username == username);
            UserModel userModel = new UserModel(user);
            return userModel.Role;
        }

        [HttpPost]
        [Route("/{currentTab}")]
        public void SetCurrentTab(string currentTab)
        {
            if (currentTab == null)
                _httpContextAccessor.HttpContext.Session.SetString("CurrentTab", "No content");
            else
                _httpContextAccessor.HttpContext.Session.SetString("CurrentTab", currentTab);
        }

        public string GetCurrentTab()
        {
            string str = _httpContextAccessor.HttpContext.Session.GetString("CurrentTab");
            if (_httpContextAccessor.HttpContext.Session.GetString("CurrentTab") == null)
                return "No content";
            return _httpContextAccessor.HttpContext.Session.GetString("CurrentTab");
        }
    }
}
