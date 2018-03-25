using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        int Commit();
        ApplicationContext CurrentContext();
    }
}
