using System.Text;
using CoreApi.Extensions;
using HDDT_BE.Interfaces.Auth;
using HDDT_BE.Interfaces.Core;
using HDDT_BE.Models.Settings;
using HDDT_BE.Services.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using JwtSettings = HDDT_BE.Models.Settings.JwtSettings;


namespace HDDT_BE.Installers
{
   public class MvcInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            var jwtSettings = new JwtSettings();
            configuration.Bind(nameof(jwtSettings), jwtSettings);
            services.AddSingleton(jwtSettings);
            
            

            services.AddSingleton<IJwtUtils, JwtUtils>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            var appApiKey = new AppApiKey();
            configuration.Bind(nameof(appApiKey), appApiKey);
            services.AddSingleton(appApiKey);
            
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = false,
                ValidateLifetime = true
            };

            services.AddSingleton(tokenValidationParameters);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = async (context) =>
                    {
                        var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
                        //var data = context.HttpContext.RequestServices.ge<IHttpContextAccessor>();
                        var userId = context.Principal.GetLoggedInUserId();
                        var user = await userService.GetById(userId);
                        if (user == null )
                        {

                            // return unauthorized if user no longer exists
                            context.Fail("Unauthorized");

                        }
                        else
                        {
                            if(user != null)
                                context.HttpContext.Items["User"] = user;
                        }
                    }
                };
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = tokenValidationParameters;
            });
        }
    }
}