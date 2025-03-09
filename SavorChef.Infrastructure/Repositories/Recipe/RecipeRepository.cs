using Microsoft.EntityFrameworkCore;
using SavorChef.Domain.Entities;
using SavorChef.Domain.Exceptions;

namespace SavorChef.Infrastructure.Repositories.Recipe;

public class RecipeRepository(DataContext dataContext) : IRecipeRepository
{
    private readonly DataContext _dataContext = dataContext;
    
    public async Task<RecipeEntity> GetRecipeAsync(int id)
    {
        return await _dataContext.Recipes.SingleOrDefaultAsync(r => r.Id == id)
               ?? throw RecipeExceptions.NotFound.ById(id);
    }

    public async Task<ICollection<RecipeEntity>> GetRecipesAsync()
    {
        return await _dataContext.Recipes.ToListAsync();
    }
    
    public async Task<RecipeEntity> CreateRecipeAsync(RecipeEntity recipe)
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
    
    public async Task<RecipeEntity> UpdateRecipeAsync(RecipeEntity recipe)
    {
        var result = await GetRecipeAsync(recipe.Id);

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
    
    public async Task DeleteRecipeAsync(int id)
    {
        var result = await GetRecipeAsync(id);

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