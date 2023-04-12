using Helper.Response;
using LOgic.Services.FriendshipService;
using LOgic.Services.UserAct;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using System.ComponentModel.DataAnnotations;

namespace AphTask.api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/profile")]
    public class ProfileController
    {
        private readonly IUserPostService _postService;
        private readonly IUserFriend _friendService;

        public ProfileController(IUserPostService postService,
            IUserFriend friendService)
        {
            _postService = postService;
            _friendService = friendService;
        }

        [HttpGet("getposts")]
        public async Task<GenericResponse> GetAllPost()
        {
            return await _postService.GetAllPost();
        }

        //[HttpGet("{postId}",Name ="getpost")]
        //public async Task<GenericResponse> GetPost(string postId)
        //{
        //    return await _postService.GetPost(postId);
        //}

        [HttpPost("addpost")]
        public GenericResponse AddPost([FromQuery] PostModel model)
        {
           return _postService.AddPost(model);
        }

        [HttpDelete("deletepost")]
        public async Task<GenericResponse> DeletePost([Required] string postId)
        {
            return await _postService.DeletePost(postId);
        }

        [HttpPost("addfriend")]
        public async Task<GenericResponse> AddFriend([Required] string userName)
        {
            return await _friendService.AddFriend(userName);
        }
    }
}
