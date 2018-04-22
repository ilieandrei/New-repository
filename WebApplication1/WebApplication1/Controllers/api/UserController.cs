using DataLayer.Entities;
using DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApplication1.Controllers.api
{
    public class UserController : ApiController
    {
        private readonly IGenericRepository<User, Guid> _userRepository;

        public UserController(IGenericRepository<User, Guid> userRepository)
        {
            _userRepository = userRepository;
        }
        
        [Route("/{username}")]
        public string GetUserRole(string username)
        {
            var user = _userRepository.GetAll().FirstOrDefault(x => x.Username == username);
            if (user != null)
                return user.Role;
            return null;
        }
    }
}
