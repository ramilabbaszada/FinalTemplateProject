using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Message;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Cashing;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Business;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using Core.Aspects.Autofac.Transaction;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;

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
        //[CasheAspect(10)]
        [LogAspect(typeof(FileLogger))]
        public async Task<IDataResult<List<Product>>> getAll()
        {
            return await _productDal.GetAll();
        }

        [LogAspect(typeof(FileLogger))]
        public async Task<IDataResult<Product>> getProductById(int id)
        {
            return await _productDal.Get(p=>p.ProductId== id);
        }

        [LogAspect(typeof(FileLogger))]
        public async Task<IDataResult<List<Product>>> getAllByCategotyId(int id)
        {
            return await _productDal.GetAll(p=>p.CategoryId==id);
        }

        [LogAspect(typeof(FileLogger))]
        public async Task<IDataResult<List<Product>>> getAllByUnitPrice(int min, int max)
        {
            return await _productDal.GetAll(p => p.UnitPrice <= max && p.UnitPrice >= min);

        }

        [LogAspect(typeof(FileLogger))]
        public async Task<IDataResult<List<ProductDetailDto>>> GetProductDetails()
        {
            return await _productDal.GetProductDetails();
        }

        [ValidationAspect(typeof(ProductValidator))]
        [SecuredOperation("product.add,admin")]
        [CacheRemoveAspect("IProductService.Get")]
        [TransactionAspect]
        [LogAspect(typeof(FileLogger))]
        public async Task<IResult> add(Product product) 
        {
            IResult result=BusinessRules.Run( await CheckIfProductCountOfCategoryCorrect(product.CategoryId),
                await CheckIfProductNameExists(product.ProductName),await CheckCategoryLimit());

            if (result != null)
            {
                return result;
            }

            return await _productDal.Add(product);
        }
        
        private async Task<IResult> CheckIfProductCountOfCategoryCorrect(int categoryId) {

            IDataResult<List<Product>> productsData = await _productDal.GetAll(p => p.CategoryId == categoryId);
            if (!productsData.Success)
                return new ErrorResult(productsData.Message);

            var count = productsData.Data.Count();

            if (count >= 10)
                return new ErrorResult(Messages.ProductCountOfCategoryError);

            return new SuccessResult();
        }

        private async Task<IResult> CheckIfProductNameExists(string productName)
        {

            IDataResult<List<Product>> productsData = await _productDal.GetAll(p => p.ProductName == productName);
            if (!productsData.Success)
                return new ErrorResult(productsData.Message);

            var result = productsData.Data.Any();

            if (result)
                return new ErrorResult(Messages.ProductNameExistsInDatabase);

            return new SuccessResult();
        }

        private async Task<IResult> CheckCategoryLimit()
        {

            var categoryResult = await _categoryService.GetAll();

            if (!categoryResult.Success)
                return new ErrorResult(categoryResult.Message);

            int count = categoryResult.Data.Count();

            if (count >= 15)
                return new ErrorResult(Messages.EnaughCategories);

            return new SuccessResult();
        }
    }
}
