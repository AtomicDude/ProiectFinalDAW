using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProiectFinalDAW.Repositories.CategoryRepository;
using ProiectFinalDAW.Models;

namespace ProiectFinalDAW.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private ICategoryRepository categoryRepository;

        public CategoryController(ICategoryRepository categoryC)
        {
            categoryRepository = categoryC;
        }

        [HttpGet("{category}")]
        public IActionResult Get(string category)
        {
            var category_name = categoryRepository.GetByCategory(category);
            if (category_name == null)
                return BadRequest(new { message = "Categoria introdusa nu exista" });
            return Ok(category_name);
        }

        [HttpPost]
        public IActionResult Post(Category category)
        {
            var new_category = new Category
            {
                Category_Name = category.Category_Name,
                Description = category.Description
            };

            categoryRepository.Create(new_category);
            var result = categoryRepository.Save();

            if (result)
                return Ok();
            else
                return BadRequest(new { message = "Eroare" });
        }
    }
}
