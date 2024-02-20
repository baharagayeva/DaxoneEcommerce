using Business.Abstract;
using Core.Helpers.Constants;
using Core.Helpers.Results.Abstract;
using Core.Helpers.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete.TableModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDAL _categoryDAL;
        private readonly IValidator<Category> _validationRules;

        public CategoryManager(ICategoryDAL categoryDAL, IValidator<Category> validationRules)
        {
            _categoryDAL = categoryDAL;
            _validationRules = validationRules;
        }
        public IDataResult<List<string>> Add(Category category)
        {
            var result = _validationRules.Validate(category);
            if (!result.IsValid)
            {
                return new ErrorDataResult<List<string>>(result.Errors.Select(x => x.PropertyName).ToList(), result.Errors.Select(x => x.ErrorMessage).ToList());
            }
            _categoryDAL.Add(category);
            return new SuccessDataResult<List<string>>(null, CommonOperationMessages.DataAddedSuccessfully);
        }

        public IResult Delete(Category category)
        {
            _categoryDAL.Update(category);
            return new SuccessResult(CommonOperationMessages.DataDeletedSuccessfully);
        }

        public IDataResult<List<Category>> GetAll()
        {
            var data = _categoryDAL;
            return new SuccessDataResult<List<Category>>(_categoryDAL.GetAll(x => x.Deleted == 0));
        }

        public IDataResult<Category> GetById(int id)
        {
            return new SuccessDataResult<Category>(_categoryDAL.GetCategory(x => x.ID == id && x.Deleted == 0));
        }

        public IDataResult<List<string>> Update(Category category)
        {
            var result = _validationRules.Validate(category);
            if (!result.IsValid)
            {
                return new ErrorDataResult<List<string>>(result.Errors.Select(x => x.PropertyName).ToList(), result.Errors.Select(x => x.ErrorMessage).ToList());
            }
            _categoryDAL.Update(category);
            return new SuccessDataResult<List<string>>(null, CommonOperationMessages.DataUpdatedSuccessfully);
        }
    }
}
