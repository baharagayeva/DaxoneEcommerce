using AutoMapper;
using Business.Abstract;
using Core.Helpers.Constants;
using Core.Helpers.Results.Abstract;
using Core.Helpers.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete.DTOs.CategoryDTOs;
using Entities.Concrete.DTOs.ColorDTOs;
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
        private readonly IMapper _mapper;

        public ColorManager(IColorDAL colorDAL, IValidator<Color> validationRules, IMapper mapper)
        {
            _colorDAL = colorDAL;
            _validationRules = validationRules;
            _mapper = mapper;
        }
        public IDataResult<List<string>> Add(AddToColorDTO addToColorDTO)
        {
            Color color = _mapper.Map<Color>(addToColorDTO);
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

        public IDataResult<List<ListToColorDTO>> GetAll()
        {
            List<Color> colorList = _colorDAL.GetAll(x => x.Deleted == 0);
            colorList.Sort((x, y) => y.ID.CompareTo(x.ID));
            return new SuccessDataResult<List<ListToColorDTO>>(_mapper.Map<List<ListToColorDTO>>(colorList));
        }

        public IDataResult<Color> GetById(int id)
        {
            return new SuccessDataResult<Color>(_colorDAL.GetColor(x => x.ID == id && x.Deleted == 0));
        }

        public IDataResult<List<string>> Update(UpdateToColorDTO updateToColorDTO)
        {
            Color color = _mapper.Map<Color>(updateToColorDTO);
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
