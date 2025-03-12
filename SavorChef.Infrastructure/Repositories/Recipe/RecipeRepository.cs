using Microsoft.EntityFrameworkCore;
using SavorChef.Domain.Entities;
using SavorChef.Domain.Exceptions;

namespace SavorChef.Infrastructure.Repositories.Recipe;

public class RecipeRepository(DataContext dataContext) : IRecipeRepository
{
    private readonly DataContext _dataContext = dataContext;
    
    public async Task<RecipeEntity> GetByIdAsync(int id)
    {
        return await _dataContext.Recipes.SingleOrDefaultAsync(r => r.Id == id)
               ?? throw RecipeExceptions.NotFound.ById(id);
    }

    public async Task<ICollection<RecipeEntity>> GetAllAsync()
    {
        return await _dataContext.Recipes.ToListAsync();
    }
    
    public async Task<RecipeEntity> CreateAsync(RecipeEntity recipe)
    {
        var recipeEntity = await _dataContext.Recipes.AddAsync(recipe);

        try
        {
            await _dataContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw RecipeExceptions.Create.Failed(e);
        }

        return recipeEntity?.Entity ?? throw RecipeExceptions.Create.Failed();
    }
    
    public async Task<RecipeEntity> UpdateAsync(RecipeEntity recipe)
    {
        var result = await GetByIdAsync(recipe.Id);

        try
        {
            _dataContext.Entry(result).CurrentValues.SetValues(recipe);
            await _dataContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw RecipeExceptions.Update.Failed(recipe.Id, e);
        }

        return result;
    }
    
    public async Task DeleteAsync(int id)
    {
        var result = await GetByIdAsync(id);

        try
        {
            _dataContext.Recipes.Remove(result);
            await _dataContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw RecipeExceptions.Delete.Failed(e);
        }
    }
}