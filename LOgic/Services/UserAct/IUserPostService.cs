using Data.Models;
using Helper.Response;
using Models.Models;

namespace LOgic.Services.UserAct
{
    public interface IUserPostService
    {
        //Task<GenericResponse> AddFriend(string userName);
        GenericResponse AddPost(PostModel model);
        Task<GenericResponse> DeletePost(string postId);
        Task<GenericResponse> GetAllPost();
        //Task<GenericResponse> GetPost(string postId);
    }
}