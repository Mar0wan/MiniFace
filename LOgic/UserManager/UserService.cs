using AutoMapper;
using Data.Models;
using Data.UOW;
using GenericException;
using Helper.enums;
using Helper.Response;
using LOgic.AuthenticationManager;
using LOgic.Services.OTP;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using Models.Dtos;
using Models.Entities;
using Models.enums;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace LOgic.UserManager
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IOTPService _oTPService;
        private readonly IStringLocalizer<LanguageResource> _localizer;
        private readonly IConfiguration _configuration;
        private readonly IAuthService _authenticationService;

        public UserService(IUnitOfWork uow,
            IAuthService authenticationService,
            IConfiguration configuration,
            IStringLocalizer<LanguageResource> localizer,
            IOTPService oTPService,
            IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
            _localizer = localizer;
            _oTPService = oTPService;
            _configuration = configuration;
            _authenticationService = authenticationService;
        }

        public async Task<GenericResponse> Register(RegistModel model)
        {
            if (await _authenticationService.UserExists(model.UserName))
                return new GenericResponse(_localizer["AlreadyExists"].Value, new { Result.Fail }, HttpStatusCode.Conflict);

            //var user = new User()
            //{
            //    Email = model.UserName,
            //    UserName = model.UserName,
            //};//mapper
            var user = _mapper.Map<User>(model);

            user.UserRole = new List<IdentityUserRole<string>>() {
                        new IdentityUserRole<string>()
                        {
                            RoleId = Role.General.GetHashCode().ToString(),
                            UserId = user.Id,
                        }
            };

            dynamic res = await _authenticationService.AddUser(user, model.Password);

            if (!res.Succeeded)
                //throw new ApiException(new BusinessException(_localizer["RegisterFailed"], HttpStatusCode.InternalServerError));
                return new GenericResponse(_localizer["RegisterFailed"], new {Result.Fail});
            var otp = await _oTPService.Generate(model.MobileNumber);
            ///sending otp by sms or whatsapp &) 
            ///
            return new GenericResponse(_localizer["Registered"].Value, Result.Success);
        }

        public async Task<GenericResponse> SignIn(SignInModel model)
        {
            var loggedUser = await _authenticationService.SignIn(model.UserName, model.Password);
            if (loggedUser == null)
                return new GenericResponse(_localizer["InvalidUsernameOrPassword"].Value, new { Result.Fail }, HttpStatusCode.NotFound);

            string role = await _authenticationService.GetUserRole(loggedUser);
            if (role == null)
                return new GenericResponse(_localizer["RoleNotFound"], HttpStatusCode.NotFound);

            var token = GenerateToken(loggedUser.Id, loggedUser.UserName);
            return new GenericResponse(_localizer["UserLogged"].Value, new LoggedInDto() { Token = token, Role = role }, HttpStatusCode.OK);
        }

        public async Task<GenericResponse> VerifyOtp(VerifyOtpModel model)
        {
            if (!await _oTPService.Verify(model.Phone, model.Otp))
                return new GenericResponse(_localizer["InvalidOtp"].Value, HttpStatusCode.BadRequest);
            var user =_authenticationService.UserByPhone(model.Phone).Result;
            var role =_authenticationService.GetUserRole(user).Result;
            var token = GenerateToken(user.Id, user.UserName);
            return new GenericResponse(_localizer["UserLogged"].Value, new LoggedInDto() { Token = token, Role = role }, HttpStatusCode.OK);

            //return new GenericResponse(_localizer["UserLogged"].Value, HttpStatusCode.OK);
        }

        private string GenerateToken(string Id, string userName)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, Id),
                    new Claim(ClaimTypes.Name, userName)
                };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Token:Secret").Value));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = credentials
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var generatedToken = tokenHandler.WriteToken(token);
            return generatedToken;
        }
    }
}
