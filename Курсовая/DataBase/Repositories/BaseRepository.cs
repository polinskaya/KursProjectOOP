using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Курсовая.DataBase.RepositoryInterfaces;

namespace Курсовая.DataBase.Repositories
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DataBaseEntities _context;
        private readonly DbSet<TEntity> _dbSet;

        protected BaseRepository(DataBaseEntities context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public TEntity Create(TEntity item)
        {
            var newItem = _dbSet.Add(item);
            _context.SaveChanges();
            return newItem;
        }

        public TEntity FindById(int id)
        {
            return _dbSet.Find(id);
        }

        public Task<TEntity> FindByIdAsync(int id)
        {
            return _dbSet.FindAsync(id);
        }

        public IEnumerable<TEntity> FindAll(Func<TEntity, bool> predicate = null,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = Include(includeProperties);
            return predicate == null ? query.ToList() : query.Where(predicate).ToList();
        }

        public void Update(TEntity item)
        {
            _context.SaveChanges();
        }

        public void Remove(TEntity item)
        {
            _context.Entry(item).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        private IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _dbSet;
            return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }
    }
}