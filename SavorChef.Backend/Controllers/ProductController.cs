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
    public class ProductController : ControllerBase
    {
        private readonly ApiContext _context;

        public ProductController(ApiContext context)
        {
            _context = context;
        }

        // Create
        [HttpPost]
        public IActionResult Create(ProductCreateRequestDto productCreateRequestDto)
        {
            var productEntity = _context.Products.Add(new ProductEntity
            {
                Name = productCreateRequestDto.Name,
                Description = productCreateRequestDto.Description
            });

            _context.SaveChanges();
            return new OkObjectResult(productEntity.Entity);
        }

        // Get
        [HttpGet]
        public IActionResult Get(int id)
        {
            var result = _context.Products.Find(id);

            if (result == null)
                return new NotFoundResult();

            return new OkObjectResult(result);
        }

        // Delete
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var result = _context.Products.Find(id);

            if (result == null)
                return new NotFoundResult();

            _context.Products.Remove(result);
            _context.SaveChanges();

            return new OkObjectResult(NoContent());
        }

        // GetAll
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _context.Products.ToList();

            return new OkObjectResult(result);
        }

    }
}