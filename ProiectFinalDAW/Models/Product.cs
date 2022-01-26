using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProiectFinalDAW.Models.Base;

namespace ProiectFinalDAW.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }

        public int BarCode { get; set; }

        public string Description { get; set; }

        public int Price { get; set; }

        public bool Active { get; set; }

        public Category Category {get; set;}

        public Guid? CategoryId { get; set; }

        public virtual ICollection<OrderDetail> Order_Details { get; set; }
    }
}
