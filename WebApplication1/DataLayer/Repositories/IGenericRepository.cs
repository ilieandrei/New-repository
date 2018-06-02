using System;
using System.Linq;
using System.Linq.Expressions;

namespace DataLayer.Repositories
{
    public interface IGenericRepository<T, TKey> where T : BaseEntity
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetAllNoTracking();
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Delete(T entity);
        void Edit(T entity);
        void Save();
    }
}