using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Курсовая.DataBase.RepositoryInterfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        TEntity Create(TEntity item);

        TEntity FindById(int id);

        Task<TEntity> FindByIdAsync(int id);

        IEnumerable<TEntity> FindAll(Func<TEntity, bool> predicate = null,
            params Expression<Func<TEntity, object>>[] includeProperties);

        void Update(TEntity item);

        void Remove(TEntity item);
    }
}