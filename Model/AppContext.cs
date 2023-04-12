using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public partial class AppContext : IdentityDbContext
    {
        public AppContext (DbContextOptions<AppContext> options) : base(options)
        { }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Friendship> Friendships { get; set; }


        //public virtual DbSet<UserRole> UserRoles { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ModelCreating();
            base.OnModelCreating(builder);
        }
    }
}
