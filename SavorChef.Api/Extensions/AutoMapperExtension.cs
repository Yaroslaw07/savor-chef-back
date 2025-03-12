using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using SavorChef.Application.Mappings;

namespace SavorChef.API.Extensions
{
    public static class AutoMapperExtension
    {
        public static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(RecipeMappingProfile), typeof(UserMappingProfile));
            return services;
        }
    }
}