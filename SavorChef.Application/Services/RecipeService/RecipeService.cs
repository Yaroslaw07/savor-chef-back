using AutoMapper;
using SavorChef.Application.Dtos.Requests;
using SavorChef.Application.Dtos.Responses;
using SavorChef.Domain.Entities;
using SavorChef.Infrastructure.Repositories.Recipe;

namespace SavorChef.Application.Services.RecipeService;

public class RecipeService : IRecipeService
{
    private readonly IRecipeRepository _recipeRepository;
    private readonly IMapper _mapper;

    public RecipeService(IRecipeRepository recipeRepository, IMapper mapper)
    {
        _recipeRepository = recipeRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<RecipeResponseDto>> GetAllAsync()
    {
        var recipes = await _recipeRepository.GetAllAsync();

        return _mapper.Map<IEnumerable<RecipeResponseDto>>(recipes);
    }

    public async Task<RecipeResponseDto> GetByIdAsync(int id)
    {
        var recipe = await _recipeRepository.GetByIdAsync(id);

        return _mapper.Map<RecipeResponseDto>(recipe);
    }
    
    public async Task<RecipeResponseDto> CreateAsync(RecipeRequestDto recipeDto)
    {
        var recipe = _mapper.Map<RecipeEntity>(recipeDto);
        var createdRecipe = await _recipeRepository.CreateAsync(recipe);

        return _mapper.Map<RecipeResponseDto>(createdRecipe);
    }
    
    public async Task<RecipeResponseDto> UpdateAsync(int id, RecipeRequestDto recipeDto)
    {
        var recipe = _mapper.Map<RecipeEntity>(recipeDto);
        recipe.Id = id;
        var updatedRecipe = await _recipeRepository.UpdateAsync(recipe);

        return _mapper.Map<RecipeResponseDto>(updatedRecipe);
    }
    
    public async Task<bool> DeleteAsync(int id)
    {
        await _recipeRepository.DeleteAsync(id);

        return true;
    }
}