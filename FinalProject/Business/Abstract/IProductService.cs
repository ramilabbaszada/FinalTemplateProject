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
        Task<IDataResult<List<Product>>> GetAll();
        Task<IDataResult<Product>> GetProductById(int id);
        Task<IDataResult<List<Product>>> GetAllByCategotyId(int id);
        Task<IDataResult<List<Product>>>GetAllByUnitPrice(int min,int max);
        Task<IDataResult<List<ProductDetailDto>>> GetProductDetails();
        Task<IResult> Add(Product p);
        Task<IResult> Update(Product product);
    }
}
