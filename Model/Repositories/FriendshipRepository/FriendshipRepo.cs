using Data.Repository;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.FriendshipRepository
{
    public class FriendshipRepo : Repository<Friendship> , IFriendshipRepo
    {
        public FriendshipRepo(AppContext context) : base(context)
        {        }
    }
}
