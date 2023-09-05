using DataAccess.Abstract;
using Entities.Concrete;
using Core.EntityFramework;
using System.Collections.Generic;
using Entities.DTOs;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : EfEntityRepositoryBase<Product, NorthwindContext>, IProductDal
    {
        public async Task<List<ProductDetailDto>> GetProductDetails()
        {
            
                using (NorthwindContext context = new NorthwindContext())
                {
                    var result = from p in context.Products
                                 join c in context.Categories
                                 on p.CategoryId equals c.CategoryID
                                 select new ProductDetailDto
                                 {
                                     ProductId = p.ProductId,
                                     ProductName = p.ProductName,
                                     CategoryName = c.CategoryName,
                                     UnitsInStock = p.UnitsInStock
                                 };
                    return new List<ProductDetailDto>(await result.ToListAsync());
                }
        }
    }
}
