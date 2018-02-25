using DataLayer.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        User GetSingle(Guid userId);
    }
}
