using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectFinalDAW.Models.DTOs
{
    public class NewOrderDTO
    {
        public ICollection<NewOrderProductDTO> Products { get; set; }
        public string Address { get; set; }
        public string Phone_Number { get; set; }
    }
}
