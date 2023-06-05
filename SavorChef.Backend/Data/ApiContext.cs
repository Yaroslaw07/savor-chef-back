using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SavorChef.Backend.Data.Entities;
using SavorChef.Backend.Data.Enums;

namespace SavorChef.Backend.Data;

public class ApiContext : DbContext
{
    public ApiContext(DbContextOptions<ApiContext> options)
        : base(options)
    {
    }

    public DbSet<RecipeEntity> Recipes { get; set; }
    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<UserEntity> Users { get; set; }


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
            .WithMany(e => e.CreatedRecipes)
            .HasForeignKey(e => e.UserId)
            .IsRequired();


        modelBuilder.Entity<UserEntity>()
            .HasMany(p => p.FavoriteRecipes)
            .WithMany(p => p.UsersThatAddedToFavorites)
            .UsingEntity(
                //l => l.HasOne(typeof(UserEntity)).WithMany().HasForeignKey("UserId"),
                //  .HasPrincipalKey(nameof(UserEntity.Id)),
               // r => r.HasOne(typeof(RecipeEntity)).WithMany().HasForeignKey("RecipeId"),
                j=>
                {
                    j.Property("FavoriteRecipesId").HasColumnName("RecipeId");
                    j.Property("UsersThatAddedToFavoritesId").HasColumnName("UserId");
                    j.ToTable("FavoriteRecipes");
                    
                });
                   // .HasPrincipalKey(nameof(RecipeEntity.Id)),
         //       j => j.HasKey("UserId", "RecipeId"));
        
        // modelBuilder.Entity<RecipeEntity>()
        //     .HasMany(p => p.UsersThatAddedToFavorites)
        //     .WithMany(p => p.FavoriteRecipes);


        
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