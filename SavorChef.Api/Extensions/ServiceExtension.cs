using SavorChef.Application.Services.AuthService;
using SavorChef.Application.Services.RecipeService;
using SavorChef.Application.Services.UserService;

namespace SavorChef.API.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            
            services.AddScoped<IRecipeService, RecipeService>();
            services.AddScoped<IUserService, UserService>();
            
            return services;
        }
    }
}