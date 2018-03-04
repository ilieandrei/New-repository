using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.Entity.Infrastructure;
using DataLayer.Users;
using WebApplication1.Models;
using DataLayer;
using DataLayer.Repositories;

namespace WebApplication1.api
{
    
    public class AuthenticationController : ApiController
    {
        //User Login
        [Route("/{username}/{password}")]
        public HttpStatusCode LoginUser(string username, string password)
        {
            UserRepository userRepository = new UserRepository();
            var user = userRepository.GetAll().Where(x => x.Username == username).FirstOrDefault();
            if (user == null)
            {
                return HttpStatusCode.NotFound;
            }
            else if(user.Password != password)
            {
                return HttpStatusCode.Unauthorized;
            }
            return HttpStatusCode.Found;
        }
        
        //Check if User exists
        [Route("/{username}")]
        public bool UserExists(string username)
        {
            UserRepository userRepository = new UserRepository();
            var user = userRepository.GetAll().Where(x => x.Username == username).FirstOrDefault();
            if (user != null)
            {
                return true;
            }
            return false;
        }

        //Register User
        [HttpPost]
        [Route("/{username}/{password}")]
        public HttpStatusCode RegisterUser(string username, string password)
        {
            if (ModelState.IsValid)
            {
                UserRepository userRepository = new UserRepository();
                User user = new User
                {
                    Id = Guid.NewGuid(),
                    Username = username,
                    Password = password
                };
                userRepository.Add(user);
                userRepository.Save();
                return HttpStatusCode.Created;
            }
            else
            {
                return HttpStatusCode.NotFound;
            }
        }
    }
}