using SavorChef.Backend.Data.Dtos;
using SavorChef.Backend.Data.Enums;

namespace SavorChef.Backend.Data.Entities;

public class RecipeEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty; //the name of the dish that will be displayed on the website.

    //  public string Ingredients { get; set; }=string.Empty; // a description of the dish that should include the main ingredients,
    // a brief history of its origin, or any other interesting information about the dish.
    public string RecipeDescription { get; set; } = string.Empty;
    public string PreparationInstructions { get; set; } = string.Empty;
    public TimeSpan PreparationTime { get; set; }
    public Difficulty Difficulty { get; set; }
    public string DishCategory { get; set; } = string.Empty;

    // Add a navigation property for the associated ProductEntity.
    public ICollection<ProductEntity> AssociatedProducts { get; set; } = new List<ProductEntity>();


    public int UserId { get; set; }
    public UserEntity UserEntity { get; set; } = null!;


    public ICollection<UserEntity> UsersThatAddedToFavorites { get; set; } = new List<UserEntity>();

    public RecipeResponseDto ToDto(string? userEmail = null)
    {
        return new RecipeResponseDto
        {
            Id = Id,
            Name = Name,
            RecipeDescription = RecipeDescription,
            PreparationInstructions = PreparationInstructions,
            PreparationTime = PreparationTime,
            Difficulty = Difficulty,
            DishCategory = DishCategory,
            Products = AssociatedProducts.Select(x => x.ToDto()).ToList(),
            CreatedByUserId = UserId,
            AddedToFavoritesByUser = userEmail == null
                ? null
                : UsersThatAddedToFavorites.FirstOrDefault(x => x.Email == userEmail) != null
        };
    }
}