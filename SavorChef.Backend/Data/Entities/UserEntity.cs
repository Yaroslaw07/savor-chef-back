namespace SavorChef.Backend.Data.Entities;

public class UserEntity
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;

    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public ICollection<RecipeEntity> CreatedRecipes { get; set; } = new List<RecipeEntity>();

    public ICollection<RecipeEntity> FavoriteRecipes { get; set; } = new List<RecipeEntity>();
}