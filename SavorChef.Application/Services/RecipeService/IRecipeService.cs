using SavorChef.Application.Dtos.Requests;
using SavorChef.Application.Dtos.Responses;

namespace SavorChef.Application.Services.RecipeService;

public interface IRecipeService
{
    Task<IEnumerable<RecipeResponseDto>> GetAllAsync();
    Task<RecipeResponseDto> GetByIdAsync(int id);
    Task<RecipeResponseDto> CreateAsync(RecipeRequestDto recipeDto);
    Task<RecipeResponseDto> UpdateAsync(int id, RecipeRequestDto recipeDto);
    Task<bool> DeleteAsync(int id);
}
