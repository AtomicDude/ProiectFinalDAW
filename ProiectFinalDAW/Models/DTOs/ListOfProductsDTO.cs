using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectFinalDAW.Models.DTOs
{
    public class ListOfProductsDTO
    {
        public ICollection<ProductInfoDTO> Products {get;set;}
    }
}
