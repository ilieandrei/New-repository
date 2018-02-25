using DataLayer.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class UserRepository :
    GenericRepository<ApplicationContext, User>, IUserRepository
    {
        public override IQueryable<User> GetAll() => Context.Users;

        public User GetSingle(Guid userId) => GetAll().FirstOrDefault(x => x.Id == userId);

        public override void Add(User user) => Context.Add(user);

        public override void Delete(User user) => Context.Remove(user);

        public override void Edit(User user) => Context.Entry(user).State = EntityState.Modified;

        public override void Save() => Context.SaveChanges();
    }
}
