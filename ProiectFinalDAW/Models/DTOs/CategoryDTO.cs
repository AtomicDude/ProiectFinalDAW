using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectFinalDAW.Models.DTOs
{
    public class CategoryDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<ProductCategoryDTO> Products { get; set; }
    }
}
