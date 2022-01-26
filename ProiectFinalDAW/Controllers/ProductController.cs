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
                Category = product.Category == null ? "Nu are categorie" : product.Category.Category_Name,
                State = product.Active ? "Activ" : "Inactiv"
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

        
        [HttpPut("update")]
        [Authorization(role.Admin)]
        public IActionResult Update(UpdateProductDTO dto)
        {
            var new_product = productRepository.GetByProductBarCode(dto.BarCode);

            if (new_product == null)
                return BadRequest(new { message = "Produsul cu acest cod de bare nu exista" });

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
            }

            productRepository.Update(new_product);
            var result = productRepository.Save();

            if (result)
                return Ok();
            return BadRequest(new { message = "Eroare" });
        }

        [HttpPut("{barcode}/trigger")]
        [Authorization(role.Admin)]
        public IActionResult ProductState(int barcode)
        {
            var product = productRepository.GetByProductBarCode(barcode);

            if (product == null)
                return BadRequest(new { message = "Produsul cu acest cod de bare nu exista" });

            product.Active = !product.Active;

            productRepository.Update(product);
            var result = productRepository.Save();

            if (result)
                return Ok(new { message = product.Active ? "Produsul a fost activat" : "Produsul a fost dezactivat" });
            return BadRequest(new { message = "Eroare" });
        }

        [HttpGet("price_range")]
        public IActionResult ProductsInPriceRange(PriceRangeDTO dto)
        {
            var result = productRepository.GetAllProductsInPriceRange(dto.Low, dto.High);

            if (result == null)
                return BadRequest("Eroare");

            var outDTO = new ListOfProductsDTO()
            {
                Products = new List<ProductInfoDTO>()
            };

            foreach (var product in result)
            {
                var category = productRepository.GetByProductBarCode(product.BarCode);
                var prodDTO = new ProductInfoDTO()
                {
                    BarCode = product.BarCode,
                    Description = product.Description,
                    Price = product.Price,
                    Category = category.Category.Category_Name,
                    Name = product.Name
                };
                outDTO.Products.Add(prodDTO);
            }
            return Ok(outDTO);
        }
        /*
        [HttpDelete("{barcode}")]
        [Authorization(role.Admin)]
        public IActionResult DeleteProduct(int barcode)
        {
            var product = productRepository.GetByProductBarCode(barcode);

            if (product == null)
                return BadRequest(new { message = "Produsul cu acest cod de bare nu exista" });

            productRepository.Delete(product);

            var result = productRepository.Save();
            if (result)
            {
                return Ok(new { Message = "Produsul a fost sters" });
            }
            return BadRequest(new { Message = "Eroare" });
        }*/
    }
}
