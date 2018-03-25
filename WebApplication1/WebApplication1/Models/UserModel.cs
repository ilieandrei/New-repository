using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public partial class UserModel : IdentityUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public UserModel(UserModel user)
        {
            Username = user.Username;
            Password = user.Password;
            Role = user.Role;
        }
        public UserModel()
        { }
    }
}
