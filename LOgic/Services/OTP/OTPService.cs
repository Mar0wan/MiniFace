using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Text;
using System.Threading.Tasks;

namespace LOgic.Services.OTP
{
    public class OTPService : IOTPService
    {
        private readonly IDistributedCache _cache;

        public OTPService(IDistributedCache cache)
        {
            _cache = cache;
        }
        public async Task<string> Generate(string phone)
        {
            Random generator = new Random();
            string val = generator.Next(0, 1000000).ToString("D6");
            byte[] value = Encoding.UTF8.GetBytes(val);

            var options = new DistributedCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(60));

            await _cache.SetAsync(phone, value, options);
            return val;
        }

        public async Task<bool> Verify(string phone, string otp)
        {
            var value = await _cache.GetAsync(phone);

            if (value != null && otp == Encoding.UTF8.GetString(value))
                return true;

            return false;
        }
    }
}