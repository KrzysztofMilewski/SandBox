using System;
using System.Linq;
using System.Linq.Expressions;

namespace Infrastructure.DataAccess
{
    public interface IEntityRepository<TEntity> where TEntity : class
    {
        TEntity GetSingleById(int id);
        TEntity GetSingleById(int id, params Expression<Func<TEntity, object>>[] includes);

        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includes);

        void Create(TEntity entity);
        TEntity CreateAndReturn(TEntity entity);

        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
