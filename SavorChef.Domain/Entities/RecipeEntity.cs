using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SavorChef.Api.Data.Enums;

namespace SavorChef.Domain.Entities;

public class RecipeEntity
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; } = string.Empty;
    public string RecipeDescription { get; set; } = string.Empty;
    public string PreparationInstructions { get; set; } = string.Empty;
    public TimeSpan PreparationTime { get; set; }
    public Difficulty Difficulty { get; set; }
    public string DishCategory { get; set; } = string.Empty;
    
    [Required]
    public int UserId { get; set; }
    
    [ForeignKey(nameof(UserId))]
    public UserEntity UserEntity { get; set; } = null!;
    
    public ICollection<UserEntity> UsersThatAddedToFavorites { get; set; } = new List<UserEntity>();
}