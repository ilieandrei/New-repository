using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Auth
{
    public interface IAuthHelper
    {
        User GetUser();
    }
}
