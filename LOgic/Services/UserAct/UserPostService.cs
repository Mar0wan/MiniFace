using AutoMapper;
using Data.UOW;
using Helper.enums;
using Helper.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Models.Dtos;
using Models.Entities;
using Models.Models;
using System.Net;
using System.Security.Claims;

namespace LOgic.Services.UserAct
{
    public class UserPostService : IUserPostService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<LanguageResource> _localizer;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private User _user;
        private object image;
        public UserPostService(IUnitOfWork uow,
            IMapper mapper,
            IStringLocalizer<LanguageResource> localizer,
            IHttpContextAccessor httpContextAccessor)
        {
            _uow = uow;
            _mapper = mapper;
            _localizer = localizer;
            _httpContextAccessor = httpContextAccessor;


            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            _user = _uow.UserManager.FindByIdAsync(userId).Result;
        }

        //public async Task<GenericResponse> GetPost(string postId)//add validation
        //{
        //    var post = _uow._postRepo.Get(e => e.Id == postId && e.UserId == _user.Id).FirstOrDefault();
        //    //if (post == null)
        //    //    return new GenericResponse(_localizer["PostRetrived"].Value, new { post });

        //    var outPost = _mapper.Map<PostDto>(post);
        //    //System.Drawing.Image.FromStream();
        //    //post."data:image;base64," + Convert.ToBase64String(arr);
        //    using (MemoryStream ms = new MemoryStream(post.Image))
        //    {
        //        outPost.Image = System.Drawing.Image.FromStream(ms);
        //    }
        //    return new GenericResponse(_localizer["PostRetrived"].Value, new { outPost });

        //}

        public async Task<GenericResponse> GetAllPost()//add validation
        {
            var posts = _uow._postRepo.Get()
                .Where(e => e.User.Friends.Select(z => z.UserId).Contains(_user.Id));
            var outPosts = _mapper.Map<IEnumerable<PostDto>>(posts);

            //System.Drawing.Image.FromStream();
            //post."data:image;base64," + Convert.ToBase64String(arr);
            foreach (var post in outPosts)
            {
                foreach (var entPost in posts)
                {
                    using (MemoryStream ms = new MemoryStream(entPost.Image))
                    {
                        post.Image = System.Drawing.Image.FromStream(ms);
                    }
                }
            }

            return new GenericResponse(_localizer["PostsRetrived"].Value,new { Result.Success , outPosts }, HttpStatusCode.OK);

        }

        public GenericResponse AddPost(PostModel model)//add validation
        {
            if (model == null)
                return new GenericResponse(_localizer["FailedToPost"].Value ,Result.Fail);

            Post post = new Post();
            post.ImageTitle = model.Image.FileName;

            MemoryStream ms = new MemoryStream();
            model.Image.CopyTo(ms);
            post.Image = ms.ToArray();

            ms.Close();
            ms.Dispose();

            post.Text = model.Text;
            post.UserId = _user.Id;
            post.Id = Guid.NewGuid().ToString();
            _uow._postRepo.Create(post);
            if (_uow.SaveChanges())
                return new GenericResponse(_localizer["PostCreated"].Value ,Result.Success);//your post has been added 
            return new GenericResponse(_localizer["FailedToPost"].Value ,Result.Fail);

        }

        public async Task<GenericResponse> DeletePost(string postId)
        {
            var post = _uow._postRepo.Delete(postId);

            if (await _uow.SaveChangesAsync())
                return new GenericResponse(_localizer["Deleted"].Value,Result.Success);//your post  has been deleted
            return new GenericResponse(_localizer["FailedToDelete"].Value ,Result.Fail);

        }

       
    }
}
