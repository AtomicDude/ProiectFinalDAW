using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProiectFinalDAW.Repositories.CategoryRepository;
using ProiectFinalDAW.Models;
using ProiectFinalDAW.Models.DTOs;
using ProiectFinalDAW.Utility;

namespace ProiectFinalDAW.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private ICategoryRepository categoryRepository;

        public CategoryController(ICategoryRepository categoryR)
        {
            categoryRepository = categoryR;
        }

        [HttpGet("{category}")]
        public IActionResult Get(string category)
        {
            var category_name = categoryRepository.GetProducts(category);
            if (category_name == null)
                return BadRequest(new { message = "Categoria introdusa nu exista" });

            var category_products = new CategoryDTO
            {
                Name = category_name.Category_Name,
                Description = category_name.Description,
                Products = new List<ProductCategoryDTO>()
            };

            foreach (var product in category_name.Products)
            {
                var productDTO = new ProductCategoryDTO()
                {
                    BarCode = product.BarCode,
                    Name = product.Name,
                    Price = product.Price
                };
                category_products.Products.Add(productDTO);
            }
            return Ok(category_products);
        }

        [HttpPost]
        [Authorization(role.Admin)]
        public IActionResult Post(Category category)
        {
            var category_name = categoryRepository.GetByCategory(category.Category_Name);
            if (category_name != null)
                return BadRequest(new { message = "Categoria introdusa exista" });

            var new_category = new Category
            {
                Category_Name = category.Category_Name,
                Description = category.Description
            };

            categoryRepository.Create(new_category);
            var result = categoryRepository.Save();

            if (result)
                return Ok(new { message = "Categoria a fost introdusa"});
            else
                return BadRequest(new { message = "Eroare" });
        }

        [HttpPut("update")]
        [Authorization(role.Admin)]
        public IActionResult UpdateCategory(UpdateCategoryDTO dto)
        {
            var new_category = categoryRepository.GetByCategory(dto.Name);
            
            if (new_category == null)
                return BadRequest(new { message = "Categoria introdusa nu exista" });

            if (!string.IsNullOrEmpty(dto.Description))
            {
                new_category.Description = dto.Description;
            }

            categoryRepository.Update(new_category);
            var result = categoryRepository.Save();

            if (result)
                return Ok(new { message = "Categoria a fost modificata" });
            return BadRequest(new { message = "Eroare" });
        }
    }
}
