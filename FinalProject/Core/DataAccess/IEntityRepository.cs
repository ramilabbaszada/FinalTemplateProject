using Core.Entities;
using Core.Entities.Abstract;
using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.DataAccess
{
    public interface IEntityRepository<T> where T:  IEntity, new()
    {
        Task<IDataResult<List<T>>> GetAll(Expression<Func<T,bool>> filter=null);
        Task<IDataResult<T>> Get(Expression<Func<T, bool>> filter) ;
        Task<IResult> Add(T entity);
        Task<IResult> Update(T entity);
        Task<IResult> Delete(T entity);
    }
}
