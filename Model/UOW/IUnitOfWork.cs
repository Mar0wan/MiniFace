using Data.Repositories.FriendshipRepository;
using Data.Repositories.UserRepository;
using Data.Repository;
using Microsoft.AspNetCore.Identity;
using Models.Entities;

namespace Data.UOW
{
    public interface IUnitOfWork
    {
        RoleManager<IdentityRole> RoleManager { get; }
        UserManager<User> UserManager { get; }
        IPostRepo _postRepo { get; }
        IFriendshipRepo _friendshipRepo { get; }

        IRepository<TRepository> GetRepository<TRepository>() where TRepository : class;
        bool SaveChanges();
        Task<bool> SaveChangesAsync();
    }
}