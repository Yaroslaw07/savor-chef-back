using SavorChef.Backend.Data.Enums;

namespace SavorChef.Backend.Data.Dtos;

public class RecipeCreateRequestDto
{
    public string Name { get; set; } = string.Empty; //the name of the dish that will be displayed on the website.

    public string Ingredients { get; set; } =
        string.Empty; // a description of the dish that should include the main ingredients,

    // a brief history of its origin, or any other interesting information about the dish.
    public string RecipeDescription { get; set; } = string.Empty;
    public string PreparationInstructions { get; set; } = string.Empty;
    public string PreparationTime { get; set; } = string.Empty;

    //[Newtonsoft.Json.JsonConverter(typeof(JsonStringEnumConverter))]

    public Difficulty Difficulty { get; set; }
    public string DishCategory { get; set; } = string.Empty;

    public List<int> AssociatedProductIds { get; set; } = new();
}