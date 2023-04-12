using Data.Repositories.FriendshipRepository;
using Data.Repositories.TeacherRepository;
using Data.Repositories.UserRepository;
using Data.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Models.Entities;

namespace Data.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppContext _context;
        private IServiceProvider _serviceProvider { get; set; }

        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private IPostRepo _postRepo;
        private IFriendshipRepo _friendshipRepo;

        public UnitOfWork(AppContext context, IServiceProvider serviceProvider, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _serviceProvider = serviceProvider;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public UserManager<User> UserManager => _userManager;
        public RoleManager<IdentityRole> RoleManager => _roleManager;


        IPostRepo IUnitOfWork._postRepo
        {
            get
            {
                if (_postRepo == null)
                {
                    _postRepo = new PostRepo(_context);
                }
                return _postRepo;
            }
        }

        IFriendshipRepo IUnitOfWork._friendshipRepo
        {
            get
            {
                if (_friendshipRepo == null)
                {
                    _friendshipRepo = new FriendshipRepo(_context);
                }
                return _friendshipRepo;
            }
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0 ? true : false;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0 ? true : false;
        }

        public IRepository<TRepository> GetRepository<TRepository>() where TRepository : class
        {
            return _serviceProvider.GetRequiredService<IRepository<TRepository>>();
        }
    }
}
