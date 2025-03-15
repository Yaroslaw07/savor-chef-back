using System.Text.Json.Serialization;
using SavorChef.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: false);
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true);
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

builder.Services.AddDataAccess(builder.Configuration).
                 AddApplicationServices().
                 AddAutoMapperProfiles().
                 AddJwtAuthentication(builder.Configuration);

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

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseCors("LocalCors");
}

if (app.Environment.IsProduction())
{
    app.UseCors("ProductionCors");
}

app.MapControllers();

app.Run();

