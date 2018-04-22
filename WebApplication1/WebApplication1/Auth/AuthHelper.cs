using DataLayer.Entities;
using DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Auth
{
    public class AuthHelper : IAuthHelper
    {
        private readonly IGenericRepository<User, Guid> _userRepository;
        public AuthHelper(IGenericRepository<User, Guid> userRepository)
        {
            _userRepository = userRepository;
        }
        public User GetUser()
        {
            return _userRepository.GetAll().FirstOrDefault();
        }
    }
}
