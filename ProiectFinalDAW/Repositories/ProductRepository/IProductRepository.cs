using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProiectFinalDAW.Repositories.GenericRepository;
using ProiectFinalDAW.Models;

namespace ProiectFinalDAW.Repositories.ProductRepository
{
    public interface IProductRepository:IGenericRepository<Product>
    {
        Product GetByProductBarCode(int BarCode);
        List<Product> GetAllProductsInPriceRange(int low, int high);
        ICollection<Tuple<string,int>> GetNumberOfProductsFromEachCategory();
    }
}
