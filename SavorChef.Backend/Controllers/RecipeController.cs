using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SavorChef.Backend.Data;
using SavorChef.Backend.Data.Dtos;
using SavorChef.Backend.Data.Entities;
using SavorChef.Backend.Services;

namespace SavorChef.Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RecipeController : ControllerBase
{
    private readonly ApiContext _context;
    private readonly IJWTService _jwtService;

    public RecipeController(ApiContext context, IJWTService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }

    //Create
    [Authorize]
    [HttpPost]
    [Route("Create")]
    public IActionResult Create(RecipeCreateRequestDto recipeCreateRequestDto)
    {
        var userEmail = _jwtService.GetCallerEmailFromRequest(Request);

        if (userEmail == null) return new BadRequestResult();

        var user = _context.Users.FirstOrDefault(x => x.Email == userEmail);
        if (user == null) return new BadRequestResult();
        var productEntities = new List<ProductEntity>();


        foreach (var productId in recipeCreateRequestDto.AssociatedProductIds)
        {
            var productEntity = _context.Products.FirstOrDefault(x => x.Id == productId);
            if (productEntity != null)
                productEntities.Add(productEntity);
        }

        var recipeEntity = new RecipeEntity
        {
            Name = recipeCreateRequestDto.Name,
            //Ingredients = recipeCreateRequestDto.Ingredients,
            RecipeDescription = recipeCreateRequestDto.RecipeDescription,
            PreparationInstructions = recipeCreateRequestDto.PreparationInstructions,
            PreparationTime = TimeSpan.Parse(recipeCreateRequestDto.PreparationTime),
            Difficulty = recipeCreateRequestDto.Difficulty,
            DishCategory = recipeCreateRequestDto.DishCategory,
            AssociatedProducts = productEntities,
            UserId = user.Id
        };
        var test = _context.Recipes.Add(recipeEntity).Entity;

        _context.SaveChanges();
        var responseDto = new RecipeResponseDto
        {
            Id = recipeEntity.Id,
            Name = recipeEntity.Name,
            //   Ingredients = recipeEntity.Ingredients,
            RecipeDescription = recipeEntity.RecipeDescription,
            PreparationInstructions = recipeEntity.PreparationInstructions,
            PreparationTime = recipeEntity.PreparationTime,
            Difficulty = recipeEntity.Difficulty,
            DishCategory = recipeEntity.DishCategory,
            Products = recipeEntity.AssociatedProducts.Select(x => new ProductResponseDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            }).ToList(),
            CreatedByUserId = recipeEntity.UserId
        };
        return new OkObjectResult(responseDto);
    }

    //GET

    [HttpGet]
    [Route("Get")]
    public IActionResult Get(int id)
    {
        var result = _context.Recipes.Include(x=>x.UsersThatAddedToFavorites).FirstOrDefault(x=>x.Id==id);

        if (result == null)
            return new NotFoundResult();

        return new OkObjectResult(result.ToDto(_jwtService.GetCallerEmailFromRequest(Request)));
    }

    //DELETE
    [Authorize]
    [HttpDelete]
    [Route("Delete")]
    public IActionResult Delete(int id)
    {
        var result = _context.Recipes.Find(id);

        if (result == null)
            return new NotFoundResult();

        _context.Recipes.Remove(result);
        _context.SaveChanges();

        return new NoContentResult();
    }

    //GetAll
    [HttpGet]
    [Route("GetAll")]
    public IActionResult GetAll()
    {
        var userEmail = _jwtService.GetCallerEmailFromRequest(Request);
        var result = _context.Recipes.Include(x=>x.UsersThatAddedToFavorites).ToList();

        return new OkObjectResult(result.Select(x=>x.ToDto(userEmail)).ToList());
    }


    [HttpPost]
    [Authorize]
    [Route("AddToFavorites/{recipeId}")]
    public IActionResult AddToFavorites(int recipeId)
    {
        var userEmail = _jwtService.GetCallerEmailFromRequest(Request);

        if (userEmail == null) return new BadRequestResult();

        var user = _context.Users.FirstOrDefault(x => x.Email == userEmail);
        if (user == null) return new BadRequestResult();

        var recipe = _context.Recipes.Find(recipeId);

        if (recipe == null) return new NotFoundResult();

        user.FavoriteRecipes.Add(recipe);
        _context.SaveChanges();

        return new OkResult();
    }
    
}