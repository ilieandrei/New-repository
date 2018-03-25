using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    public abstract class Entity<T> : BaseEntity, IEntity<T>, IEntityKey<T>
    {
        public virtual T Id { get; set; }
        object IEntityKey.Id
        {
            get { return Id; }
            set { Id = (T)value; }
        }
    }
    public abstract class BaseEntity
    { }
}
