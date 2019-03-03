using Infrastructure.Persistence;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Infrastructure.DataAccess
{
    public class EFRepository<TEntity> : IEntityRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IDbSet<TEntity> _entities;

        public EFRepository()
        {
            _dbContext = ApplicationDbContext.Create();
            _entities = _dbContext.Set<TEntity>();
        }

        public void Create(TEntity entity)
        {
            _entities.Add(entity);
            _dbContext.SaveChanges();
        }

        public TEntity CreateAndReturn(TEntity entity)
        {
            _entities.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public void Delete(TEntity entity)
        {
            var entityToDelete = _entities.Remove(entity);
            _dbContext.SaveChanges();
        }

        public IQueryable<TEntity> GetAll()
        {
            return _entities;
        }

        public IQueryable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> result = _entities;
            foreach (var include in includes)
            {
                result = result.Include(include);
            }
            return result;
        }

        public TEntity GetSingleById(int id)
        {
            return _entities.Find(id);
        }

        public TEntity GetSingleById(int id, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> result = _entities;
            foreach (var include in includes)
            {
                result = result.Include(include);
            }
            return result.SingleOrDefault();
        }

        public void Update(TEntity entity)
        {
            _entities.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
