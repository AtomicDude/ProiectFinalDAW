using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectFinalDAW.Models.DTOs
{
    public class ProductDTO
    {
        public int BarCode { get; set; }
        public string Product_Name { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
    }
}
