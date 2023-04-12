using AutoMapper;
using Data.Models;
using Models.Entities;

namespace LOgic.Profiles
{
    public class AuthProfile:Profile
    {
        public AuthProfile()
        {
            CreateMap<RegistModel,User >().ForMember(dest=>dest.PhoneNumber ,
                opt=>opt.MapFrom(src=>src.MobileNumber));
        }
    }
}
