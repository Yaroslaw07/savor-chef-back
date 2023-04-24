using SavorChef.Backend.Data.Entities;

namespace SavorChef.Backend.Data.Dtos;

public class ProductCreateRequestDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    
    public ICollection<RecipeEntity> AssociatedRecipes { get; set; } = new List<RecipeEntity>();


}