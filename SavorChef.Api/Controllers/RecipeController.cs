using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SavorChef.Application.Dtos.Requests;
using SavorChef.Application.Dtos.Responses;
using SavorChef.Application.Services.RecipeService;

namespace SavorChef.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RecipeController : ControllerBase
{
    private readonly IRecipeService _recipeService;
    
    public RecipeController(IRecipeService recipeService)
    {
        _recipeService = recipeService;
    }

    //Create
    [Authorize]
    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> Create(RecipeRequestDto recipeRequestDto)
    {
        var responseDto = await _recipeService.CreateAsync(recipeRequestDto);
        return new OkObjectResult(responseDto);
    }

    //GET

    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> Get(int id)
    {
        var responseDto = await _recipeService.GetByIdAsync(id);
        
        return new OkObjectResult(responseDto);
    }

    //DELETE
    [Authorize]
    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _recipeService.DeleteAsync(id);
        return new OkObjectResult(response);
    }

    //GetAll
    [HttpGet]
    [Route("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var response = await _recipeService.GetAllAsync();
        return new OkObjectResult(response);
    }
}