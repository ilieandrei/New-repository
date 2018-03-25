using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    public interface IEntityKey
    {
        object Id { get; set; }
    }
    public interface IEntityKey<TKey> : IEntityKey
    {
        TKey Id { get; set; }
    }
}
