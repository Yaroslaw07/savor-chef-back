using System.Reflection;
using Microsoft.EntityFrameworkCore;
using SavorChef.Domain.Entities;

namespace SavorChef.Infrastructure;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<RecipeEntity> Recipes { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}