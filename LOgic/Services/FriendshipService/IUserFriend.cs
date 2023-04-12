using Helper.Response;

namespace LOgic.Services.FriendshipService
{
    public interface IUserFriend
    {
        Task<GenericResponse> AddFriend(string userName);
    }
}