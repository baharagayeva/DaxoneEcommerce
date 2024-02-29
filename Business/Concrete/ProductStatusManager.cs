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
    public class ProductStatusManager : IProductStatusService
    {
        private readonly IProductStatusDAL _productStatusDAL;
        private readonly IValidator<ProductStatus> _validationRules;

        public ProductStatusManager(IProductStatusDAL productStatusDAL, IValidator<ProductStatus> validationRules)
        {
            _productStatusDAL = productStatusDAL;
            _validationRules = validationRules;
        }
        public IDataResult<List<string>> Add(ProductStatus productStatus)
        {
            var result = _validationRules.Validate(productStatus);
            if (!result.IsValid)
            {
                return new ErrorDataResult<List<string>>(result.Errors.Select(x => x.PropertyName).ToList(), result.Errors.Select(x => x.ErrorMessage).ToList());
            }
            _productStatusDAL.Add(productStatus);
            return new SuccessDataResult<List<string>>(null, CommonOperationMessages.DataAddedSuccessfully);
        }

        public IResult Delete(ProductStatus productStatus)
        {
            _productStatusDAL.Update(productStatus);
            return new SuccessResult(CommonOperationMessages.DataDeletedSuccessfully);
        }

        public IDataResult<List<ProductStatus>> GetAll()
        {
            var data = _productStatusDAL.GetAll(x => x.Deleted == 0);
            data.Reverse();
            return new SuccessDataResult<List<ProductStatus>>(data);
        }

        public IDataResult<ProductStatus> GetById(int id)
        {
            return new SuccessDataResult<ProductStatus>(_productStatusDAL.GetProductStatus(x => x.ID == id && x.Deleted == 0));
        }

        public IDataResult<List<string>> Update(ProductStatus productStatus)
        {
            var result = _validationRules.Validate(productStatus);
            if (!result.IsValid)
            {
                return new ErrorDataResult<List<string>>(result.Errors.Select(x => x.PropertyName).ToList(), result.Errors.Select(x => x.ErrorMessage).ToList());
            }
            _productStatusDAL.Update(productStatus);
            return new SuccessDataResult<List<string>>(null, CommonOperationMessages.DataUpdatedSuccessfully);
        }
    }
}
