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

            var recipeEntity = _context.Recipes.Add(new RecipeEntity
            {
                Name = recipeCreateRequestDto.Name,
                Ingredients = recipeCreateRequestDto.Ingredients,
                RecipeDescription = recipeCreateRequestDto.RecipeDescription,
                PreparationInstructions = recipeCreateRequestDto.PreparationInstructions,
                PreparationTime = recipeCreateRequestDto.PreparationTime,
                Difficulty = recipeCreateRequestDto.Difficulty,
                DishCategory = recipeCreateRequestDto.DishCategory
            });
            
            _context.SaveChanges();
            return new OkObjectResult(recipeEntity.Entity);
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
