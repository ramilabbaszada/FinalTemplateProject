using DataAccess.Abstract;
using Entities.Concrete;
using Core.EntityFramework;
using System.Collections.Generic;
using Entities.DTOs;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : EfEntityRepositoryBase<Product, NorthwindContext>, IProductDal
    {
        public async Task<IDataResult<List<ProductDetailDto>>> GetProductDetails()
        {
            try {
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
                    return new SuccessDataResult<List<ProductDetailDto>>(await result.ToListAsync());
                }
            }
            catch (Exception e) {
                return new ErrorDataResult<List<ProductDetailDto>>(e.Message+"\n"+e.StackTrace);
            }
            
            
        }
    }
}
