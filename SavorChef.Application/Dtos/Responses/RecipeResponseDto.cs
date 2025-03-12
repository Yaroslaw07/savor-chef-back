using SavorChef.Domain.Enums;

namespace SavorChef.Application.Dtos.Responses;
public class RecipeResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty; //the name of the dish that will be displayed on the website.

    public string RecipeDescription { get; set; } = string.Empty;
    public string PreparationInstructions { get; set; } = string.Empty;
    public TimeSpan PreparationTime { get; set; }
    public required RecipeDifficulty RecipeDifficulty { get; set; }
    public string DishCategory { get; set; } = string.Empty;
    public int CreatedByUserId { get; set; }
    
    public bool? AddedToFavoritesByUser { get; set; }
}