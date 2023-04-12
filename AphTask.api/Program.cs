using Data;
using FluentValidation.AspNetCore;
using GenericException.ExceptionHandling;
using Helper.Response;
using Helper.Validation;
using LOgic;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Models.Entities;
using NLog;
using NLog.Web;
using Service;
using System.Globalization;
using System.Net;
using System.Reflection;
using System.Text;
using AppContext = Data.AppContext;

var logger = NLog.LogManager
    .Setup()
    .LoadConfigurationFromAppSettings()
    .GetCurrentClassLogger();
try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddMvc(options =>
    {
        options.Filters.Add(typeof(ValidateModelAttribute));
    }).AddFluentValidation(options => options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));

    builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = c =>
        {
            var errors = c.ModelState.Keys
        .SelectMany(key => c.ModelState[key].Errors.Select(x => new ResponseMessage { Key = key, Message = x.ErrorMessage }))
        .ToList();

            return new UnprocessableEntityObjectResult(
                new ErrorResponse(errors, HttpStatusCode.UnprocessableEntity));

        };
    });
    //builder.Services.AddControllers();

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    //builder.Services.AddSwaggerGen();
    builder.Services.AddSwaggerGen(swagger =>
    {
        swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "MiniFacebookApp " });
        swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });
        swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }});
    });

    builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
    builder.Services.AddData(builder.Configuration);
    builder.Services.AddIdentity<User, IdentityRole>()
                    .AddEntityFrameworkStores<AppContext>()
                    .AddDefaultTokenProviders();

    builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(o =>
                    {
                        o.TokenValidationParameters = new TokenValidationParameters
                        {
                            RequireExpirationTime = true,
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey
                            (Encoding.ASCII.GetBytes(builder.Configuration.GetSection("Token:Secret").Value)),
                            ValidateIssuer = false,
                            ValidateAudience = false,
                        };
                    });

    builder.Services.AddControllers().AddViewLocalization().AddDataAnnotationsLocalization(options =>
    {
        options.DataAnnotationLocalizerProvider = (type, factory) =>
        {
            var assemblyName = new AssemblyName(typeof(LanguageResource).GetTypeInfo().Assembly.FullName);
            return factory.Create("LanguageResource", assemblyName.Name);
        };
    });

    builder.Services.Configure<RequestLocalizationOptions>(op =>
    {
        var supportedCultures = new List<CultureInfo>
                    {
                    new CultureInfo("ar"),
                    new CultureInfo("en")
                    };

        op.DefaultRequestCulture = new RequestCulture(culture: "en", uiCulture: "en");
        op.SupportedCultures = supportedCultures;
        op.SupportedUICultures = supportedCultures;
    });

    builder.Services.AddService(builder.Configuration);

    builder.Logging.ClearProviders();

    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);

    builder.Host.UseNLog();

    var app = builder.Build();

    app.UseMiddleware<GlobalExceptionHandler>();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    //app.UseStaticFiles();
    app.UseHttpsRedirection();

    app.UseRouting();

    app.UseRequestLocalization();

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}

catch (Exception ex)
{
    logger.Error(ex);
    throw;
}
finally
{
    // Ensure to shout downon the NLog ( Disposing )
    NLog.LogManager.Shutdown();
}