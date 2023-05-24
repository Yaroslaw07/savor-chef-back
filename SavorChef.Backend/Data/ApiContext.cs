using SavorChef.Backend.Data.Entities;

namespace SavorChef.Backend.Data;
using Microsoft.EntityFrameworkCore;

public class ApiContext: DbContext
{
    
    public DbSet<RecipeEntity> Recipes { get; set; }
    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<UserEntity> Users { get; set; }

    public ApiContext(DbContextOptions<ApiContext> options)
        : base(options)
    {

    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RecipeEntity>()
            .HasMany(x => x.AssociatedProducts)
            .WithMany(x => x.AssociatedRecipes);
        
        modelBuilder.Entity<ProductEntity>()
            .HasMany(x => x.AssociatedRecipes)
            .WithMany(x => x.AssociatedProducts);

        modelBuilder.Entity<RecipeEntity>()
            .HasOne(e => e.UserEntity)
            .WithMany(e => e.RecipesEntities)
            .HasForeignKey(e => e.UserId)
            .IsRequired();
    }
}   