using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectFinalDAW.Models.DTOs
{
    public class UpdateProductDTO
    {
        public int BarCode { get; set; } 

        public string Name { get; set; }

        public int Price { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }
    }
}
