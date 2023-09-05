using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Aspects.Autofac.Cashing;
using Core.Aspects.PostSharp.Logging.concrete;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        [DatabaseLogAspectAsync]
        [FileLogAspectAsync]
        public async Task<IDataResult<List<OperationClaim>>> GetClaims(User user)
        {
            return new SuccessDataResult<List<OperationClaim>>(await _userDal.GetClaims(user));
        }

        [DatabaseLogAspectAsync]
        [FileLogAspectAsync]
        public async Task<IResult> Add(User user)
        {
            await _userDal.Add(user);
            return new SuccessResult();
        }

        [DatabaseLogAspectAsync]
        [FileLogAspectAsync]
        [CasheAspect(10)]
        public async Task<IDataResult<User>> GetByMail(string email)
        {
            return new SuccessDataResult<User>(await _userDal.Get(u => u.Email == email));
        }
    }
}