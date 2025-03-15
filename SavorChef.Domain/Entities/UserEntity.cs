using System.ComponentModel.DataAnnotations;

namespace SavorChef.Domain.Entities;

public class UserEntity
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    public string Password { get; set; } = string.Empty;
    
    public ICollection<RecipeEntity> CreatedRecipes { get; set; } = new List<RecipeEntity>();

    public ICollection<RecipeEntity> FavoriteRecipes { get; set; } = new List<RecipeEntity>();
}