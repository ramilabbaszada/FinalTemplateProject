using Business.Abstract;
using Core.Aspects.Autofac.Cashing;
using Core.Aspects.PostSharp.Logging.concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        [DatabaseLogAspectAsync]
        [FileLogAspectAsync]
        [CasheAspect(10)]
        public async Task<IDataResult<List<Category>>> GetAll()
        {
            return new SuccessDataResult<List<Category>>( await _categoryDal.GetAll());
        }

        [DatabaseLogAspectAsync]
        [FileLogAspectAsync]
        public async Task<IDataResult<Category>> GetById(int categoryId)
        {
            return new SuccessDataResult<Category>( await _categoryDal.Get(c => c.CategoryID == categoryId));
        }

    }
}
