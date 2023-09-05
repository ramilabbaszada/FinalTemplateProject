using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;


namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var userToLogin = await _authService.Login(userForLoginDto);
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin);
            }
            var result = await _authService.CreateAccessTokenAsync(userToLogin.Data);

            return Ok(result.Data);
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            var userExists = await _authService.UserExists(userForRegisterDto.Email);
            if (userExists.Success)
                return BadRequest(userExists.Message);

            var registerResult = await _authService.Register(userForRegisterDto, userForRegisterDto.Password);

            var result = await _authService.CreateAccessTokenAsync(registerResult.Data);

            return Ok(result.Data);
        }
    }
}
