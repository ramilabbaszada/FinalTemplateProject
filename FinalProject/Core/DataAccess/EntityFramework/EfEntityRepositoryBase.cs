using Core.Entities;
using Core.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using Core.Entities.Abstract;
using System.Threading.Tasks;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using System.Reflection;

namespace Core.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        public async Task<IResult> Add(TEntity entity)
        {
            try
            {

                    using (TContext context = new TContext())
                    {
                        var addedEntity = context.Entry(entity);
                        addedEntity.State = EntityState.Added;
                        await context.SaveChangesAsync();
                    }

                return new SuccessResult();
            }
            catch (Exception e) { 
                return new ErrorResult(e.Message + "\n" + e.StackTrace);
            }
        }

        public async Task<IResult> Delete(TEntity entity)
        {
            try
            {

                using (TContext context = new TContext())
                {
                    var deletedEntity = context.Entry(entity);
                    deletedEntity.State = EntityState.Deleted;
                    await   context.SaveChangesAsync();
                }

                return new SuccessResult();
            }
            catch (Exception e) {
                return new ErrorResult(e.Message + "\n" + e.StackTrace);
            }
        }

        public async Task<IDataResult<TEntity>> Get(Expression<Func<TEntity, bool>> filter)
        {
            try
            {
                using (TContext context = new TContext())
                {
                    return new SuccessDataResult<TEntity>(await context.Set<TEntity>().Where(filter).SingleOrDefaultAsync());
                }
            }
            catch (Exception e) {
                return new ErrorDataResult<TEntity>(e.Message + "\n" + e.StackTrace);
            }
        }

        public async Task<IDataResult<List<TEntity>>> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            try
            {
                using (TContext context = new TContext())
                {
                    return filter == null ? new SuccessDataResult<List<TEntity>>(await context.Set<TEntity>().ToListAsync())
                        : new SuccessDataResult<List<TEntity>>(await context.Set<TEntity>().Where(filter).ToListAsync());
                    
                }
            }
            catch (Exception e) {
                return new ErrorDataResult<List<TEntity>> (e.Message + "\n" + e.StackTrace);
            }
        }

        public async Task<IResult> Update(TEntity entity)
        {
            try
            {
                using (TContext context = new TContext())
                {
                    var updatedEntity = context.Entry(entity);
                    updatedEntity.State = EntityState.Modified;
                    await context.SaveChangesAsync();
                }
                return new SuccessResult();
            }
            catch (Exception e) {
                return new ErrorResult(e.Message + "\n" + e.StackTrace);
            }
        }

    }
}
