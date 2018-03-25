using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Repositories
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private ApplicationContext _applicationContext;
        public UnitOfWork(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }
        public int Commit()
        {
            return _applicationContext.SaveChanges();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            if(disposing)
            {
                if(_applicationContext != null)
                {
                    _applicationContext.Dispose();
                    _applicationContext = null;
                }
            }
        }
        public ApplicationContext CurrentContext()
        {
            return _applicationContext;
        }
    }
}
