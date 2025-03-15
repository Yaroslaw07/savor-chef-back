using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace SavorChef.API.Extensions
{
    public static class AuthExtension
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = true,
                        ValidAudience = configuration["JWT:Audience"],
                        ValidateIssuer = true,
                        ValidIssuer = configuration["JWT:Issuer"],
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.ASCII.GetBytes(configuration["JWT:Secret"] ??
                                                    throw new InvalidOperationException("JWT:Secret is not configured"))),
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                });

            return services;
        }
    }
}