using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Data;

namespace DataLayer.Repositories
{
    public class GenericRepository<T, TKey> :
        IGenericRepository<T, TKey> where T : BaseEntity, IEntityKey<TKey>
    {

        protected readonly IUnitOfWork _uow;
        public GenericRepository(IUnitOfWork uow)
        {
            _uow = uow;
        }

        private ApplicationContext CurrentContext()
        {
            return _uow.CurrentContext();
        }

        public virtual IQueryable<T> GetAll() => CurrentContext().Set<T>();

        public IQueryable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate) => CurrentContext().Set<T>().Where(predicate);

        public virtual void Add(T entity) => CurrentContext().Set<T>().Add(entity);

        public virtual void Delete(T entity) => CurrentContext().Set<T>().Remove(entity);

        public virtual void Edit(T entity) => CurrentContext().Entry(entity).State = EntityState.Modified;

        public virtual void Save() => CurrentContext().SaveChanges();
    }
}