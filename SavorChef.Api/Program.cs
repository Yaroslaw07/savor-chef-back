using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SavorChef.API.Extensions;
using SavorChef.Application.Services.AuthService.Hash;
using SavorChef.Application.Services.AuthService.Jwt;
using SavorChef.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json");

builder.Services.AddDbContext<DataContext>
    (opt => opt.UseNpgsql(builder.Configuration["ConnectionStrings:Postgres"]));

builder.Services.AddApplicationServices().
                 AddRepositories().
                 AddAutoMapperProfiles();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidAudience = "31231",
            ValidateIssuer = true,
            ValidIssuer = "123",
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secret12345678abcdef")),
            ValidateLifetime = true
        };
    });


builder.Services.AddSingleton<IJwtService, JwtService>();
builder.Services.AddSingleton<IHasher, Hasher>(_ => new Hasher(builder.Configuration["Salt"]));

builder.Services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("LocalCors",
        policy =>
        {
            policy
                .AllowAnyOrigin() // .WithOrigins("http://localhost:3000", "https://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
if (app.Environment.IsDevelopment())
{
    app.UseCors("LocalCors");
}

if (app.Environment.IsProduction())
{
    app.UseCors("ProductionCors");
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

