using Data.Repositories.UserRepository;
using Data.Repository;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Repositories.TeacherRepository
{
    public class PostRepo : Repository<Post> ,IPostRepo
    {

        public PostRepo(AppContext context) : base(context)
        {    }
    }
}
