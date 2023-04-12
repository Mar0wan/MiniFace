namespace LOgic.Services.OTP
{
    public interface IOTPService
    {
        Task<string> Generate(string phone);
        Task<bool> Verify(string phone, string otp);
    }
}