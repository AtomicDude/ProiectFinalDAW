using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProiectFinalDAW.Models.Base;


namespace ProiectFinalDAW.Models
{
    public class FavouriteAddress:BaseEntity
    {   
        public string Fav_Address { get; set; }

        public virtual User User { get; set; }
    }
}
