using System;
using System.Collections.Generic;

namespace SWD392_EventManagement.IRepository
{
    public interface IRepository<TEntity, TKey>
    {
        TEntity GetById(TKey id);
        IEnumerable<TEntity> GetAll();
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TKey id);

    }
}
