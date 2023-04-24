namespace SavorChef.Backend.Data.Entities;

public class RecipeEntity
{
    public int Id { get; set; } 
    public string Name { get; set; }=string.Empty; //the name of the dish that will be displayed on the website.
    public string Ingredients { get; set; }=string.Empty; // a description of the dish that should include the main ingredients,
                                                          // a brief history of its origin, or any other interesting information about the dish.
    public string RecipeDescription { get; set; }=string.Empty;
    public string PreparationInstructions { get; set; } = string.Empty;
    public string PreparationTime { get; set; }=string.Empty;
    public string Difficulty { get; set; }=string.Empty;
    public string DishCategory { get; set; }=string.Empty;
    
    // Add a foreign key for the ProductEntity.
    public int ProductEntityId { get; set; }

    // Add a navigation property for the associated ProductEntity.
    public ProductEntity AssociatedProduct { get; set; }


}