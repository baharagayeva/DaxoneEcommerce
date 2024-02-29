using Business.Abstract;
using Core.Helpers.Constants;
using Core.Helpers.Results.Abstract;
using Core.Helpers.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete.TableModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class SubCategoryManager : ISubCategoryService
    {
        private readonly ISubCategoryDAL _subCategoryDAL;
        private readonly IValidator<SubCategory> _validationRules;

        public SubCategoryManager(ISubCategoryDAL subCategoryDAL, IValidator<SubCategory> validationRules)
        {
            _subCategoryDAL = subCategoryDAL;
            _validationRules = validationRules;
        }
        public IDataResult<List<string>> Add(SubCategory subCategory)
        {
            var result = _validationRules.Validate(subCategory);
            if (!result.IsValid)
            {
                return new ErrorDataResult<List<string>>(result.Errors.Select(x => x.PropertyName).ToList(), result.Errors.Select(x => x.ErrorMessage).ToList());
            }
            _subCategoryDAL.Add(subCategory);
            return new SuccessDataResult<List<string>>(null, CommonOperationMessages.DataAddedSuccessfully);
        }

        public IResult Delete(SubCategory subCategory)
        {
            _subCategoryDAL.Update(subCategory);
            return new SuccessResult(CommonOperationMessages.DataDeletedSuccessfully);
        }

        public IDataResult<List<SubCategory>> GetAll()
        {
            var data = _subCategoryDAL.GetAll(x => x.Deleted == 0);
            data.Reverse();
            return new SuccessDataResult<List<SubCategory>>(data);
        }

        public IDataResult<SubCategory> GetById(int id)
        {
            return new SuccessDataResult<SubCategory>(_subCategoryDAL.Get(x => x.ID == id && x.Deleted == 0));
        }

        public IDataResult<List<string>> Update(SubCategory subCategory)
        {
            var result = _validationRules.Validate(subCategory);
            if (!result.IsValid)
            {
                return new ErrorDataResult<List<string>>(result.Errors.Select(x => x.PropertyName).ToList(), result.Errors.Select(x => x.ErrorMessage).ToList());
            }
            _subCategoryDAL.Update(subCategory);
            return new SuccessDataResult<List<string>>(null, CommonOperationMessages.DataUpdatedSuccessfully);
        }
    }
}
