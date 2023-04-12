using Data.Models;
using Helper.Response;

namespace LOgic.UserManager
{
    public interface IUserService
    {
        Task<GenericResponse> Register(RegistModel model);
        Task<GenericResponse> SignIn(SignInModel model);
        Task<GenericResponse> VerifyOtp(VerifyOtpModel model);
    }
}