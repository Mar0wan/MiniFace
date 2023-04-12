using AutoMapper;
using Data.Models;
using FluentValidation;
using LOgic.AuthenticationManager;
using LOgic.Services.FriendshipService;
using LOgic.Services.OTP;
using LOgic.Services.UserAct;
using LOgic.UserManager;
using LOgic.UserManager.ViewModels.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace Service
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddTransient<IValidator<RegistModel>, RegistValidator>();
            services.AddTransient<IValidator<SignInModel>, SignInValidator>();

            return services;
        }
        public static IServiceCollection AddService(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddUtilities(configuration);
            //services.AddIntegration(configuration);
            services.AddValidators();
            //services.AddHttpContextAccessor();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            //services.AddScoped(provider => new MapperConfiguration(cfg =>
            //{
            //    cfg.AddProfile(new MappingProfile());
            ////}).CreateMapper());
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IOTPService, OTPService>();
            services.AddTransient<IUserPostService, UserPostService>();
            services.AddTransient<IUserFriend, UserFriend>();

            //services.AddTransient<IUserService, UserService>();
            //services.AddTransient<ILookupsService, LookupsService>();
            //services.AddTransient<IClassesService , ClassesService>();
            //services.AddTransient<ICoursesServices, CoursesServices>();
            //services.AddTransient<IStudentsService, StudentsService>();
            //services.AddTransient<IStudentCourseService, StudentCourseService>();
            //services.AddTransient<ISchoolsServices,SchoolsServices>();
            //services.AddTransient<ITeacherClassesService,TeacherClassesService>();
            //services.AddTransient<ITeacherCoursesService,TeacherCoursesService>();
            //services.AddTransient<ITeachersService, TeachersService>();
            //services.AddTransient<IUploadService, UploadService>();
            //services.AddTransient<IStudyPhaseService , StudyPhaseService>();
            //services.AddTransient<ISubPhaseService, SubPhaseService>();
            return services;
        }
    }
}
