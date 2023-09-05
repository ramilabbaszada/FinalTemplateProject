using Core.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using Core.Entities.Abstract;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Core.Extensions;

namespace Core.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        public async Task Add(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                await context.SaveChangesAsync();
            }
        }

        public async Task Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                await   context.SaveChangesAsync();
            }
        }

        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                return await context.Set<TEntity>().Where(filter).SingleOrDefaultAsync();
            }
        }

        public async Task<List<TEntity>> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                return filter == null ? new List<TEntity>(await context.Set<TEntity>().ToListAsync())
                    : new List<TEntity>(await context.Set<TEntity>().Where(filter).ToListAsync());     
            }
        }

        public async Task Update(TEntity entity,Action<EntityEntry<TEntity>>rules)
        {
            using (TContext context = new TContext())
            {
                var updatedEntity = context.Entry(entity);

                if (rules == null)
                {
                    updatedEntity.State = EntityState.Modified;
                    goto summary;
                }

                foreach (var property in typeof(TEntity).GetProperties().Where(propery => !propery.IsEditable()))
                    updatedEntity.Property(property.Name).IsModified = false;

                rules(updatedEntity);

                summary:
                await context.SaveChangesAsync();
            }
        }

    }
}
