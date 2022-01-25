using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectFinalDAW.Models.DTOs
{
    public class UpdateOrderStatusDTO
    {
        public int Order_Number { get; set; }
        public string Status { get; set; }
    }
}
