using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public async Task<IDataResult<List<OperationClaim>>> GetClaims(User user)
        {
            return await _userDal.GetClaims(user);
        }

        public async Task<IResult> Add(User user)
        {
            return await _userDal.Add(user);
        }

        public async Task<IDataResult<User>> GetByMail(string email)
        {
            return await _userDal.Get(u => u.Email == email);
        }
    }
}