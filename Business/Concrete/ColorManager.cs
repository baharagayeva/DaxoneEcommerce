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
    public class ColorManager : IColorService
    {
        private readonly IColorDAL _colorDAL;
        private readonly IValidator<Color> _validationRules;

        public ColorManager(IColorDAL colorDAL, IValidator<Color> validationRules)
        {
            _colorDAL = colorDAL;
            _validationRules = validationRules;
        }
        public IDataResult<List<string>> Add(Color color)
        {
            var result = _validationRules.Validate(color);
            if (!result.IsValid)
            {
                return new ErrorDataResult<List<string>>(result.Errors.Select(x => x.PropertyName).ToList(), result.Errors.Select(x => x.ErrorMessage).ToList());
            }
            _colorDAL.Add(color);
            return new SuccessDataResult<List<string>>(null, CommonOperationMessages.DataAddedSuccessfully);
        }

        public IResult Delete(Color color)
        {
            _colorDAL.Update(color);
            return new SuccessResult(CommonOperationMessages.DataDeletedSuccessfully);
        }

        public IDataResult<List<Color>> GetAll()
        {
            var data = _colorDAL.GetAll(x => x.Deleted == 0);
            data.Reverse();
            return new SuccessDataResult<List<Color>>(data);
        }

        public IDataResult<Color> GetById(int id)
        {
            return new SuccessDataResult<Color>(_colorDAL.GetColor(x => x.ID == id && x.Deleted == 0));
        }

        public IDataResult<List<string>> Update(Color color)
        {
            var result = _validationRules.Validate(color);
            if (!result.IsValid)
            {
                return new ErrorDataResult<List<string>>(result.Errors.Select(x => x.PropertyName).ToList(), result.Errors.Select(x => x.ErrorMessage).ToList());
            }
            _colorDAL.Update(color);
            return new SuccessDataResult<List<string>>(null, CommonOperationMessages.DataUpdatedSuccessfully);
        }
    }
}
