using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectFinalDAW.Models.DTOs
{
    public class NoProdInCategDTO
    {
        public ICollection<List<(string Category_Name, int No_Prod)>> Categories { get; set; }
    }
}
