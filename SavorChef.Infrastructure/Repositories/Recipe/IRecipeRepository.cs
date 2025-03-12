
using SavorChef.Domain.Entities;

namespace SavorChef.Infrastructure.Repositories.Recipe;

public interface IRecipeRepository
{
    public Task<RecipeEntity> GetByIdAsync(int id);
    
    public Task<ICollection<RecipeEntity>> GetAllAsync();

    public Task<RecipeEntity> CreateAsync(RecipeEntity recipe);
    
    public Task<RecipeEntity> UpdateAsync(RecipeEntity recipe);
    
    public Task DeleteAsync(int id);
}