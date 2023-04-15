using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SavorChef.Backend.Repositories;
using SavorChef.Backend.Services;
using Microsoft.EntityFrameworkCore;
using SavorChef.Backend.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ApiContext>
    (opt => opt.UseInMemoryDatabase("RecipeDb"));   
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>  
{  
    options.AddPolicy(name: "LocalCors",  
        policy  =>  
        {  
            policy
                .AllowAnyOrigin()   // .WithOrigins("http://localhost:3000", "https://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });  
    // options.AddPolicy(name: "ProductionCors",  
    //     policy  =>  
    //     {  
    //         policy
    //             .WithOrigins("https://production-website.com")
    //             .AllowAnyHeader()
    //             .AllowAnyMethod();
    //     });  
});
builder.Services.AddSingleton<IUserRepository, InMemoryUserRepository>();
builder.Services.AddSingleton<IJWTService, JWTService>();
//scoped transient
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

var app = builder.Build();

// Configure the HTTP request pipeline.
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
else
{
    // app.UseCors("ProductionCors");
}
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();