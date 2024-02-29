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
    public class SeasonDiscountManager : ISeasonDiscountService
    {
        private readonly ISeasonDiscountDAL _seasonDiscountDAL;
        private readonly IValidator<SeasonDiscount> _validationRules;

        public SeasonDiscountManager(ISeasonDiscountDAL seasonDiscountDAL, IValidator<SeasonDiscount> validationRules)
        {
            _seasonDiscountDAL = seasonDiscountDAL;
            _validationRules = validationRules;
        }
        public IDataResult<List<string>> Add(SeasonDiscount seasonDiscount)
        {
            var result = _validationRules.Validate(seasonDiscount);
            if (!result.IsValid)
            {
                return new ErrorDataResult<List<string>>(result.Errors.Select(x => x.PropertyName).ToList(), result.Errors.Select(x => x.ErrorMessage).ToList());
            }
            _seasonDiscountDAL.Add(seasonDiscount);
            return new SuccessDataResult<List<string>>(null, CommonOperationMessages.DataAddedSuccessfully);
        }

        public IResult Delete(SeasonDiscount seasonDiscount)
        {
            _seasonDiscountDAL.Update(seasonDiscount);
            return new SuccessResult(CommonOperationMessages.DataDeletedSuccessfully);
        }

        public IDataResult<List<SeasonDiscount>> GetAll()
        {
            var data = _seasonDiscountDAL.GetAll(x => x.Deleted == 0);
            data.Reverse();
            return new SuccessDataResult<List<SeasonDiscount>>(data);
        }

        public IDataResult<SeasonDiscount> GetById(int id)
        {
            return new SuccessDataResult<SeasonDiscount>(_seasonDiscountDAL.Get(x => x.ID == id && x.Deleted == 0));
        }

        public IDataResult<List<string>> Update(SeasonDiscount seasonDiscount)
        {
            var result = _validationRules.Validate(seasonDiscount);
            if (!result.IsValid)
            {
                return new ErrorDataResult<List<string>>(result.Errors.Select(x => x.PropertyName).ToList(), result.Errors.Select(x => x.ErrorMessage).ToList());
            }
            _seasonDiscountDAL.Update(seasonDiscount);
            return new SuccessDataResult<List<string>>(null, CommonOperationMessages.DataUpdatedSuccessfully);
        }
    }
}
