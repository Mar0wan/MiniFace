using Data.UOW;
using Microsoft.AspNetCore.Identity;
using Models.Entities;

namespace LOgic.AuthenticationManager
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        public AuthService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IdentityResult> AddUser(User user, string password)
        {
            user.PasswordHash = _unitOfWork.UserManager.PasswordHasher.HashPassword(user, password);
            // await _unitOfWork.UserManager.UpdateSecurityStampAsync(user);
            return await _unitOfWork.UserManager.CreateAsync(user);
        }

        public async Task<User> SignIn(string userName, string password)
        {
            var user = await _unitOfWork.UserManager.FindByNameAsync(userName);
            if (user != null && await CheckPassword(user, password))
            {
                return user;
            }
            return null;
        }

        public async Task<User> UserByPhone (string phone)////
        {
            var user = await _unitOfWork.UserManager.FindByEmailAsync(phone) ;
            if (user != null )
            {
                return user;
            }
            return null;
        }

        public async Task<bool> UserExists(string userName)
        {
            return (await _unitOfWork.UserManager.FindByNameAsync(userName)) != null;
        }
        public async Task<string> GetUserRole(User user)
        {
            var roles = await _unitOfWork.UserManager.GetRolesAsync(user);
            return roles[0];
        }
        public async Task<bool> CheckPassword(User user, string password)
        {
            return await _unitOfWork.UserManager.CheckPasswordAsync(user, password);
        }

    }
}
