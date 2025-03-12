using Microsoft.Extensions.DependencyInjection;
using SavorChef.Infrastructure.Repositories.Recipe;
using SavorChef.Infrastructure.Repositories.User;

namespace SavorChef.API.Extensions
{
    public static class RepositoryExtension
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRecipeRepository, RecipeRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}