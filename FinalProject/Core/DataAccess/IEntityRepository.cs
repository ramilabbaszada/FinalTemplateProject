using Core.Entities.Abstract;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.DataAccess
{
    public interface IEntityRepository<T> where T : class,IEntity, new()
    {
        Task<List<T>> GetAll(Expression<Func<T,bool>> filter=null);
        Task<T> Get(Expression<Func<T, bool>> filter) ;
        Task Add(T entity);
        Task Update(T entity,Action<EntityEntry<T>> rules=null);
        Task Delete(T entity);
    }
}
