using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface IRepository<TEntity>
       where TEntity : class
    {
        IQueryable<TEntity> Get();
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, string includeProps = null);
        Task Create(TEntity entity);
        Task Delete(object id);
        Task<TEntity> GetById(object id);
        Task<TEntity> GetByCompsiteId(object id1, object id2);
        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);
        void DeleteRange(IEnumerable<TEntity> entities);
        Task AddRange(IEnumerable<TEntity> entities);
    }
}
