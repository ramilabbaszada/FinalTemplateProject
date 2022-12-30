using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductService
    {
        Task<IDataResult<List<Product>>> getAll();
        Task<IDataResult<Product>> getProductById(int id);
        Task<IDataResult<List<Product>>> getAllByCategotyId(int id);
        Task<IDataResult<List<Product>>>getAllByUnitPrice(int min,int max);
        Task<IDataResult<List<ProductDetailDto>>> GetProductDetails();
        Task<IResult> add(Product p);
    }
}
