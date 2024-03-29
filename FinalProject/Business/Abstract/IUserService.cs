﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;

namespace Business.Abstract
{
    public interface IUserService
    {
        Task<IDataResult<User>> GetByRefreshToken(string token);
        Task<IDataResult<List<OperationClaim>>> GetClaims(User user);
        Task<IResult> Add(User user);
        Task<IDataResult<User>> GetByMail(string email);
    }
}
