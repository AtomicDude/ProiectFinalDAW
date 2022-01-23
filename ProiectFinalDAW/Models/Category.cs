using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProiectFinalDAW.Models.Base;


namespace ProiectFinalDAW.Models
{
    public class Category:BaseEntity
    {
        public string Category_Name { get; set; }

        public string Description { get; set; }
 
        public virtual ICollection<Product> Products { get; set; }
    }
}
