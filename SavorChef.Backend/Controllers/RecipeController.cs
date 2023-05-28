using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SavorChef.Backend.Data;
using SavorChef.Backend.Data.Dtos;
using SavorChef.Backend.Data.Entities;
using SavorChef.Backend.Services;

namespace SavorChef.Backend.Controllers
{
    [Route("api/[controller]/[action]")]
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
        public IActionResult Create(RecipeCreateRequestDto recipeCreateRequestDto)
        {
            var userEmail = _jwtService.GetCallerEmailFromRequest(Request);

            if (userEmail == null)
            {
                return new BadRequestResult();
            }

            var user = _context.Users.FirstOrDefault(x => x.Email == userEmail);
            if (user == null)
            {
                return new BadRequestResult();
            }
            var productEntities = new List<ProductEntity>();
            

            foreach (var productId in recipeCreateRequestDto.AssociatedProductIds)
            {
                var productEntity = _context.Products.FirstOrDefault(x => x.Id == productId);
                if(productEntity!=null)
                    productEntities.Add(productEntity);
            }

            var recipeEntity = new RecipeEntity
            {
                Name = recipeCreateRequestDto.Name,
                //Ingredients = recipeCreateRequestDto.Ingredients,
                RecipeDescription = recipeCreateRequestDto.RecipeDescription,
                PreparationInstructions = recipeCreateRequestDto.PreparationInstructions,
                PreparationTime = recipeCreateRequestDto.PreparationTime,
                Difficulty = recipeCreateRequestDto.Difficulty,
                DishCategory = recipeCreateRequestDto.DishCategory,
                AssociatedProducts = productEntities,
                UserId = user.Id
            };
            var test=_context.Recipes.Add(recipeEntity).Entity;
            
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
                UserId = recipeEntity.UserId

            };
            return new OkObjectResult(responseDto);
        }
        
        //GET
       
        [HttpGet]
        public IActionResult Get(int id)
        {
            var result = _context.Recipes.Find(id);

            if (result == null)
                return new NotFoundResult();

            return new OkObjectResult(result);
        }
        
        //DELETE
        [Authorize]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var result = _context.Recipes.Find(id);

            if (result == null)
                return new NotFoundResult();

            _context.Recipes.Remove(result);
            _context.SaveChanges();

            return new OkObjectResult(NoContent());
        }
        
        //GetAll
        [HttpGet()]

        public IActionResult GetAll()
        {
            var result = _context.Recipes.ToList();

            return new OkObjectResult(result);
        }

    }
}
