using AutoMapper;
using Data.UOW;
using Helper.enums;
using Helper.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Models.Entities;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LOgic.Services.FriendshipService
{
    public class UserFriend : IUserFriend
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<LanguageResource> _localizer;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private User _user;

        public UserFriend(IUnitOfWork uow,
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

        public async Task<GenericResponse> AddFriend(string userName)
        {
            var friend = await _uow.UserManager.FindByNameAsync(userName);
            if (friend.Id == _user.Id)
                return new GenericResponse(_localizer["InvalidUserName"].Value,new { Result.Fail } , HttpStatusCode.BadRequest);

            if (friend == null)
                return new GenericResponse(_localizer["UserNotFound"].Value, new { Result.Fail }, HttpStatusCode.NotFound);

            if (await _uow._friendshipRepo.GetByCompsiteId(_user.Id, friend.Id) != null ||
                await _uow._friendshipRepo.GetByCompsiteId(friend.Id, _user.Id) != null)
                return new GenericResponse(_localizer["AlreadyFriends"].Value, new { Result.Fail }, HttpStatusCode.BadRequest);

            var friendship = new Friendship();
            friendship.UserId = _user.Id;
            friendship.UserFriendId = friend.Id;
            await _uow._friendshipRepo.Create(friendship);
            if (!_uow.SaveChanges())
                return new GenericResponse(_localizer["FailedToAdd"].Value, new { Result.Fail }, HttpStatusCode.InternalServerError);

            return new GenericResponse(_localizer["Added"].Value,Result.Success);//your friend  has been added 

        }
    }
}
