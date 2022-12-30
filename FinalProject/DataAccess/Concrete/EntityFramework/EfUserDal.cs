using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Concrete;
using Core.EntityFramework;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, NorthwindContext>, IUserDal
    {
        public async Task<IDataResult<List<OperationClaim>>> GetClaims(User user)
        {
            try {
                using (var context = new NorthwindContext())
                {
                    var result = from operationClaim in context.OperationClaims
                                 join userOperationClaim in context.UserOperationClaims
                                     on operationClaim.Id equals userOperationClaim.OperationClaimId
                                 where userOperationClaim.UserId == user.Id
                                 select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };

                    return new SuccessDataResult<List<OperationClaim>>(await result.ToListAsync());
                }
            } catch (Exception e) {
                return new ErrorDataResult<List<OperationClaim>>(e.Message+"\n"+e.StackTrace);
            }
            
        }
    }
}