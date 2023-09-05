using Business.Abstract;
using Business.Message;
using Core.Entities.Concrete;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entities.Dtos;
using System.Threading.Tasks;
using Core.Aspects.PostSharp.Logging.concrete;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        [DatabaseLogAspectAsync]
        [FileLogAspectAsync]
        [ValidationAspect(typeof(UserForRegisterValidator))]
        public async Task<IDataResult<User>> Register(UserForRegisterDto userForRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var user = new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };

            return new SuccessDataResult<User>(user, Messages.UserRegistered);
        }

        [DatabaseLogAspectAsync]
        [FileLogAspectAsync]
        public async Task<IDataResult<User>> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck =await UserExists(userForLoginDto.Email);

            if (!userToCheck.Success)
                return userToCheck;

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.Data.PasswordHash, userToCheck.Data.PasswordSalt))
                return new ErrorDataResult<User>(Messages.PasswordError);
            
            return new SuccessDataResult<User>(userToCheck.Data, Messages.SuccessfulLogin);
        }

        [DatabaseLogAspectAsync]
        [FileLogAspectAsync]
        public async Task<IDataResult<User>> UserExists(string email)
        {
            var user = await _userService.GetByMail(email);

            if (user.Data == null)
                return new ErrorDataResult<User>(Messages.UserNotFound);

            return new SuccessDataResult<User>(user.Data);
        }

        [DatabaseLogAspectAsync]
        [FileLogAspectAsync]
        public async Task<IDataResult<AccessToken>> CreateAccessToken(User user)
        {
            var claims = await _userService.GetClaims(user);

            var accessToken = _tokenHelper.CreateToken(user, claims.Data);

            return new SuccessDataResult<AccessToken>(accessToken.Data, Messages.AccessTokenCreated);
        }
    }
}