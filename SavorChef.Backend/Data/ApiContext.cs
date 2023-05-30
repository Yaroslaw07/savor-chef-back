using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SavorChef.Backend.Data.Entities;
using SavorChef.Backend.Data.Enums;

namespace SavorChef.Backend.Data;
using Microsoft.EntityFrameworkCore;

public class ApiContext : DbContext
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



        modelBuilder.Entity<UserEntity>()
            .HasMany(p => p.AssociatedRecipesEntities)
            .WithMany(p => p.AssociatedUserEntities);

        modelBuilder.Entity<RecipeEntity>()
            .HasMany(p => p.AssociatedUserEntities)
            .WithMany(p => p.AssociatedRecipesEntities);


        modelBuilder.Entity<RecipeEntity>()
            .Property(x => x.Difficulty)
            .HasConversion(new EnumToStringConverter<Difficulty>());


        modelBuilder.Entity<UserEntity>()
            .HasIndex(x => x.Email)
            .IsUnique();


        modelBuilder.Entity<UserEntity>()
            .HasIndex(x => x.UserName)
            .IsUnique();


        modelBuilder.Entity<RecipeEntity>()
            .Property(x => x.PreparationTime)
            .HasConversion(x => x.ToString(), x => TimeSpan.Parse(x));
    }
}    