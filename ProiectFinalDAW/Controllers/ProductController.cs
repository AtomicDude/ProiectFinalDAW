using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProiectFinalDAW.Models;
using ProiectFinalDAW.Repositories.ProductRepository;
using ProiectFinalDAW.Repositories.CategoryRepository;
using ProiectFinalDAW.Models.DTOs;
using ProiectFinalDAW.Utility;

namespace ProiectFinalDAW.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private IProductRepository productRepository;
        private ICategoryRepository categoryRepository;

        public ProductController(IProductRepository productR, ICategoryRepository categoryR)
        {
            productRepository = productR;
            categoryRepository = categoryR;
        }

        [HttpGet("{barcode}")]
        public IActionResult Get(int barcode)
        {
            var product = productRepository.GetByProductBarCode(barcode);

            if (product == null)
                return BadRequest(new { message = "Produsul cu acest cod de bare nu exista" });

            var product_category = new ProductCategory2DTO
            {
                Name = product.Name,
                BarCode = product.BarCode,
                Price = product.Price,
                Description = product.Description,
                Category = product.Category.Category_Name
            };

            return Ok(product_category);
        }


        [HttpPost]
        [Authorization(role.Admin)]
        public IActionResult Post(AddNewProductDTO dto)
        {
            var exists = productRepository.GetByProductBarCode(dto.BarCode);

            if (exists != null)
                return BadRequest(new { message = "Produsul cu acest cod de bare exista" });

            var new_category = categoryRepository.GetByCategory(dto.Category);

            if (new_category != null)
            {
                var new_product = new Product
                {
                    Name = dto.Name,
                    Description = dto.Description,
                    Price = dto.Price,
                    Active = false,
                    CategoryId = new_category.Id,
                    BarCode = dto.BarCode
                };

                productRepository.Create(new_product);
                var result = productRepository.Save();

                if (result)
                {
                    new_category.Products.Add(new_product);
                    categoryRepository.Update(new_category);
                    result = categoryRepository.Save();

                    if (result)
                        return Ok();
                }
                else
                    return BadRequest(new { message = "Eroare" });
                
            }
            else
                return BadRequest(new {message = "Categoria introdusa nu exista"} );
            return BadRequest(new { message = "Eroare" });
        }

        /*
        [HttpPut("update")]
        [Authorization(role.Admin)]
        public IActionResult Update(UpdateProductDTO dto)
        {
            var new_product = productRepository.GetByProductId(dto.Id);

            if (new_product == null)
                return BadRequest(new { message = "Produsul cu id-ul introdus nu exista" });

            if (!string.IsNullOrEmpty(dto.Name))
            {
                new_product.Name = dto.Name;
            }

            if (dto.Price != 0)
            {
                new_product.Price = dto.Price;
            }

            if (!string.IsNullOrEmpty(dto.Description))
            {
                new_product.Description = dto.Description;
            }

            if (!string.IsNullOrEmpty(dto.Category))
            {
                var new_category = categoryRepository.GetByCategory(dto.Category);

                if (new_category == null)
                    return BadRequest(new { message = "Categoria introdusa nu exista" });
                new_product.CategoryId = new_category.Id;

                var old_product = productRepository.GetByProductId(dto.Id);

                if (new_product == null)
                    return BadRequest(new { message = "Produsul cu id-ul introdus nu exista" });
                
                var old_category = old_product.Products.Remove(productRepository.GetByProductId(dto.Id));
                categoryRepository.Update(new_category);
                var result = categoryRepository.Save();
                if (result)
                {
                    var second_category = categoryRepository.GetByCategory(dto.Category)
                }
            }

            //update old category
            //update new category

        }
        */

    }
}
