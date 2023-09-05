using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using System.Collections.Generic;

namespace Core.Utilities.Security.JWT
{
    public interface ITokenHelper
    {
        IDataResult<AccessToken> CreateToken(User user, List<OperationClaim> operationClaims);
    }
}
