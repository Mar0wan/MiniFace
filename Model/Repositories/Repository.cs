using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        protected readonly AppContext context;
        protected readonly DbSet<TEntity> dbSet;
        public Repository(AppContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }

        public IQueryable<TEntity> Get()
        {
            return dbSet;
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, string includeProps = null)
        {
            IQueryable<TEntity> query = dbSet;
            if (includeProps != null)
            {
                var props = includeProps.Split(",");
                foreach (var prop in props)
                {
                    query = query.Include(prop);
                }
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.AsEnumerable();
        }

        public async Task Create(TEntity entity)
        {
            await dbSet.AddAsync(entity);
        }

        public async Task Delete(object id)
        {
            dbSet.Remove(await dbSet.FindAsync(id));
        }

        public async Task<TEntity> GetById(object id)
        {
            return await dbSet.FindAsync(id);
        }
        public async Task<TEntity> GetByCompsiteId(object id1,object id2)
        {
            return await dbSet.FindAsync(id1,id2);
        }

        public void Update(TEntity entity)
        {
            dbSet.Update(entity);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            dbSet.UpdateRange(entities);
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            dbSet.RemoveRange(entities);
        }

        public async Task AddRange(IEnumerable<TEntity> entities)
        {
            await dbSet.AddRangeAsync(entities);
        }

    }
}