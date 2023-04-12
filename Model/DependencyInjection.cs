using Data.Repository;
using Data.UOW;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("Defualt"),
                b => b.MigrationsAssembly("Data")
                ));
            

            //services.AddIdentity<User, IdentityRole>()
                //.AddEntityFrameworkStores<AppContext>()
                //.AddDefaultTokenProviders();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }

    }
}
