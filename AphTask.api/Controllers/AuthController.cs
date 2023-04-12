using Data.Models;
using Helper.Response;
using LOgic;
using LOgic.UserManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Service;

namespace AphTask.api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController( IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<GenericResponse> Resgist(RegistModel model)
        {
            return (await _userService.Register(model));
        }

        [HttpPost("login")]
        public async Task<GenericResponse> SignIn(SignInModel model)
        {
            return (await _userService.SignIn(model));
        }

        [HttpPost("verifyotp")]
        public async Task<GenericResponse> Verify(VerifyOtpModel model)
        {
            return (await _userService.VerifyOtp(model));
        }
    }
}
