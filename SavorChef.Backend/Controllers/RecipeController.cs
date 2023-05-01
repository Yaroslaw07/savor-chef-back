using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SavorChef.Backend.Data;
using SavorChef.Backend.Data.Dtos;
using SavorChef.Backend.Data.Entities;

namespace SavorChef.Backend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly ApiContext _context;

        public RecipeController(ApiContext context)
        {
            _context = context;
        }

        //Create
        [HttpPost]
        public IActionResult Create(RecipeCreateRequestDto recipeCreateRequestDto)
        {
            var productEntities = new List<ProductEntity>();


            foreach (var productId in recipeCreateRequestDto.AssociatedProductIds)
            {
                var productEntity = _context.Products.FirstOrDefault(x => x.Id == productId);
                if(productEntity!=null)
                    productEntities.Add(productEntity);
            }

            var recipeEntity = _context.Recipes.Add(new RecipeEntity
            {
                Name = recipeCreateRequestDto.Name,
                Ingredients = recipeCreateRequestDto.Ingredients,
                RecipeDescription = recipeCreateRequestDto.RecipeDescription,
                PreparationInstructions = recipeCreateRequestDto.PreparationInstructions,
                PreparationTime = recipeCreateRequestDto.PreparationTime,
                Difficulty = recipeCreateRequestDto.Difficulty,
                DishCategory = recipeCreateRequestDto.DishCategory,
                AssociatedProducts = productEntities
            });
            
            _context.SaveChanges();
            var responseDto = new RecipeResponseDto
            {
                Id = recipeEntity.Entity.Id,
                Name = recipeEntity.Entity.Name,
                Ingredients = recipeEntity.Entity.Ingredients,
                RecipeDescription = recipeEntity.Entity.RecipeDescription,
                PreparationInstructions = recipeEntity.Entity.PreparationInstructions,
                PreparationTime = recipeEntity.Entity.PreparationTime,
                Difficulty = recipeEntity.Entity.Difficulty,
                DishCategory = recipeEntity.Entity.DishCategory,
                Products = recipeEntity.Entity.AssociatedProducts.Select(x => new ProductResponseDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description
                }).ToList()

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
