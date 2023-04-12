using Microsoft.AspNetCore.Identity;
using Models.Entities;

namespace LOgic.AuthenticationManager
{
    public interface IAuthService
    {
        Task<IdentityResult> AddUser(User user, string password);
        Task<bool> CheckPassword(User user, string password);
        Task<string> GetUserRole(User user);
        Task<User> SignIn(string userName, string password);
        Task<bool> UserExists(string userName);
        Task<User> UserByPhone(string phone);
    }
}