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
        // Get All The Users
        [HttpGet]
        public List<User> GetAll()
        {
            List<User> list = new List<User>();
            UserRepository userRepository = new UserRepository();
            var results = userRepository.GetAll().ToList();
            return results;
        }

        //Get User By Username
        [Route("/{username}/{password}")]
        public bool Get(string username, string password)
        {
            UserRepository userRepository = new UserRepository();
            var user = userRepository.GetAll().Where(x => x.Username == username).FirstOrDefault();
            if (user == null || user.Password != password)
            {
                return false;
                //throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.Unauthorized));
            }
            return true;
        }

        //User Login
        public bool Login(string username, string password)
        {
            using (var db = new ApplicationContext())
            {
                User user = db.Users.Find(username);
                if(user == null)
                {
                    return false;
                }
                else
                {
                    if(user.Password != password)
                    {
                        return false;
                    }
                    return true;
                }
            }
        }
        
        //Insert User
        public HttpResponseMessage Post(User user)
        {
            if (ModelState.IsValid)
            {
                using (var db = new ApplicationContext())
                {
                    UserRepository userRepository = new UserRepository();
                    userRepository.Add(user);
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
                    return response;
                }
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }
    }
}