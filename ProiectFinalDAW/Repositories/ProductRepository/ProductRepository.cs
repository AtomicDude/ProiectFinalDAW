using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProiectFinalDAW.Repositories.GenericRepository;
using ProiectFinalDAW.Models;
using ProiectFinalDAW.Data;
using Microsoft.EntityFrameworkCore;

namespace ProiectFinalDAW.Repositories.ProductRepository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(Context C) : base(C)
        {

        }

        public Product GetByProductBarCode(int BarCode)
        {
            return _table.Include(x => x.Category).FirstOrDefault(x => x.BarCode.Equals(BarCode));
        }

        public List<Product> GetAllProductsInPriceRange(int low, int high)
        {
            var result = from x in _table
                         where low <= x.Price && x.Price <= high
                         select x;
            return result.ToList();
        }

        public ICollection<Tuple<string, int>> GetNumberOfProductsFromEachCategory()
        {
            var result = from x in _context.Products
                         join y in _context.Categories on x.CategoryId equals y.Id
                         group y by y.Category_Name into g
                         select new
                         {
                             g.Key,
                             Column1 = g.Count()
                         };

            var ret_list = new List<Tuple<string, int>>();

            foreach (var x in result)
            {
                var tup = new Tuple<string, int>(x.Key, x.Column1);
                ret_list.Add(tup);
            }

            return ret_list;
        }
    }
}
