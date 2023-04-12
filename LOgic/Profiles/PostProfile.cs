using AutoMapper;
using Models.Dtos;
using Models.Entities;
using Models.Models;

namespace LOgic.Profiles
{
    public class PostProfile:Profile
    {
        public PostProfile()
        {
            CreateMap<Post, PostDto>();
            CreateMap<PostModel, Post>();
        }
    }
}
