using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SavorChef.Infrastructure;
using SavorChef.Infrastructure.Repositories.Recipe;
using SavorChef.Infrastructure.Repositories.User;

namespace SavorChef.API.Extensions
{
    public static class DataAccessExtension
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(opt => 
                opt.UseNpgsql(configuration["ConnectionStrings:Postgres"]!));
            
            services.AddScoped<IRecipeRepository, RecipeRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}