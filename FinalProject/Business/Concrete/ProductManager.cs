using Business.Abstract;
using Business.Message;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Business;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Extensions;
using Core.Aspects.Autofac.Cashing;
using Core.Aspects.PostSharp.Logging.concrete;
using Business.BusinessAspects.Autofac;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        ICategoryService _categoryService;
        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }

        [DatabaseLogAspectAsync]
        [FileLogAspectAsync]
        [CasheAspect(10)]
        public async Task<IDataResult<List<Product>>> GetAll()
        {
            return new SuccessDataResult<List<Product>>( await _productDal.GetAll());
        }

        [DatabaseLogAspectAsync]
        [FileLogAspectAsync]
        public async Task<IDataResult<Product>> GetProductById(int id)
        {
            return new SuccessDataResult<Product>( await _productDal.Get(p=>p.ProductId== id));
        }

        [DatabaseLogAspectAsync]
        [FileLogAspectAsync]
        public async Task<IDataResult<List<Product>>> GetAllByCategotyId(int id)
        {
            return new SuccessDataResult<List<Product>>( await _productDal.GetAll(p=>p.CategoryId==id));
        }

        [DatabaseLogAspectAsync]
        [FileLogAspectAsync]
        public async Task<IDataResult<List<Product>>> GetAllByUnitPrice(int min, int max)
        {
            return new SuccessDataResult<List<Product>>( await _productDal.GetAll(p => p.UnitPrice <= max && p.UnitPrice >= min));
        }
        [DatabaseLogAspectAsync]
        [FileLogAspectAsync]
        [CasheAspect(10)]
        public async Task<IDataResult<List<ProductDetailDto>>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>( await _productDal.GetProductDetails());
        }

        [DatabaseLogAspectAsync]
        [FileLogAspectAsync]
        [SecuredOperation("Manager")]
        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        public async Task<IResult> Update(Product product) {

            await _productDal.Update(product, (editingEntry) => { 
                editingEntry.SetValue((entity)=>entity.UnitPrice, product.UnitPrice);
                editingEntry.SetValue((entity) => entity.UnitsInStock, product.UnitsInStock);
                editingEntry.SetValue((entity) => entity.ProductName, product.ProductName);
                editingEntry.SetValue((entity) => entity.CategoryId, product.CategoryId);
            });

            return new SuccessResult();
        }

        //[DatabaseLogAspectAsync]
        //[FileLogAspectAsync]
        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        public async Task<IResult> Add(Product product) 
        {
            IResult result=BusinessRules.Run( await CheckIfProductCountOfCategoryCorrect(product.CategoryId),
                await CheckIfProductNameExists(product.ProductName),await CheckCategoryLimit());

            if (result != null)
                return result;
            
            await _productDal.Add(product);
            return new SuccessResult();
        }
        
        private async Task<IResult> CheckIfProductCountOfCategoryCorrect(int categoryId) {

            List<Product> productsData = await _productDal.GetAll(p => p.CategoryId == categoryId);

            var count = productsData.Count();

            if (count >= 10)
                return new ErrorResult(Messages.ProductCountOfCategoryError);

            return new SuccessResult();
        }

        private async Task<IResult> CheckIfProductNameExists(string productName)
        {
            List<Product> productsData = await _productDal.GetAll(p => p.ProductName == productName);

            var result = productsData.Any();

            if (result)
                return new ErrorResult(Messages.ProductNameExistsInDatabase);

            return new SuccessResult();
        }

        private async Task<IResult> CheckCategoryLimit()
        {
            var categoryResult = await _categoryService.GetAll();

            int count = categoryResult.Data.Count();

            if (count >= 15)
                return new ErrorResult(Messages.EnaughCategories);

            return new SuccessResult();
        }
    }
}
