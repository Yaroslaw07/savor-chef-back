
using SavorChef.Domain.Entities;

namespace SavorChef.Infrastructure.Repositories.Recipe;

public interface IRecipeRepository
{
    public Task<RecipeEntity> GetRecipeAsync(int id);
    
    public Task<ICollection<RecipeEntity>> GetRecipesAsync();

    public Task<RecipeEntity> CreateRecipeAsync(RecipeEntity recipe);
    
    public Task<RecipeEntity> UpdateRecipeAsync(RecipeEntity recipe);
    
    public Task DeleteRecipeAsync(int id);
}