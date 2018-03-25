using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}
