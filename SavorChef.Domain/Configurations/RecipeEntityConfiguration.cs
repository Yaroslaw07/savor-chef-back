using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SavorChef.Domain.Entities;
using SavorChef.Domain.Enums;

namespace SavorChef.Domain.Configurations;

public class RecipeEntityConfiguration : IEntityTypeConfiguration<RecipeEntity>
{
    public void Configure(EntityTypeBuilder<RecipeEntity> builder)
    {
        // Configure the TimeSpan conversion (can't be done with annotations)
        builder.Property(x => x.PreparationTime)
            .HasConversion(x => x.ToString(), x => TimeSpan.Parse(x));
            
        // Configure the Enum to string conversion
        builder.Property(x => x.RecipeDifficulty)
            .HasConversion(new EnumToStringConverter<RecipeDifficulty>());
            
        // Configure the many-to-many relationship with custom table name
        builder.HasMany(p => p.UsersThatAddedToFavorites)
            .WithMany(p => p.FavoriteRecipes)
            .UsingEntity(j =>
            {
                j.Property("RecipesId").HasColumnName("RecipeId");
                j.Property("UsersId").HasColumnName("UserId");
                j.ToTable("FavoriteRecipes");
            });
    }
}