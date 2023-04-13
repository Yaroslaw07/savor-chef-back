using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SavorChef.Backend.Models;
using SavorChef.Backend.Data;

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

        ///Create/Edit
        [HttpPost]
        public IActionResult CreateEdit(Recipe recipe)
        {
            if (recipe.Id == 0)
            {
                _context.Recipes.Add(recipe);
            }
            else
            {
                var resipeInDb = _context.Recipes.Find(recipe.Id);

                if (resipeInDb == null)
                    return new NotFoundResult();
            }

            _context.SaveChanges();
            return new OkObjectResult(recipe);
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
