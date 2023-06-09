using SavorChef.Backend.Data.Dtos;

namespace SavorChef.Backend.Data.Entities;

public class ProductEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public ICollection<RecipeEntity> AssociatedRecipes { get; set; } = new List<RecipeEntity>();

    public ProductResponseDto ToDto()
    {
        return new ProductResponseDto
        {
            Id = Id,
            Name = Name,
            Description = Description
        };

    }
}