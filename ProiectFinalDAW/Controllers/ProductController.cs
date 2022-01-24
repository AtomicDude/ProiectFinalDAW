using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProiectFinalDAW.Models;
using ProiectFinalDAW.Repositories.ProductRepository;

namespace ProiectFinalDAW.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private IProductRepository productRepository;

        public ProductController(IProductRepository productP)
        {
            productRepository = productP;
        }

        [HttpPost]
        public IActionResult Post(Product product)
        {
            var new_product = new Product
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Active = product.Active,
                Category = product.Category
            };

            productRepository.Create(new_product);
            var result = productRepository.Save();

            if (result)
                return Ok();
            else
                return BadRequest(new { message = "Eroare" });
        }

    }
}
