using AutoMapper;
using Business.Abstract;
using Core.Helpers.Constants;
using Core.Helpers.Results.Abstract;
using Core.Helpers.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete.DTOs.ProductStatusDTOs;
using Entities.Concrete.DTOs.SizeDTOs;
using Entities.Concrete.TableModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class SizeManager : ISizeService
    {
        private readonly ISizeDAL _sizeDAL;
        private readonly IValidator<Size> _validationRules;
        private readonly IMapper _mapper;

        public SizeManager(ISizeDAL sizeDAL, IValidator<Size> validationRules, IMapper mapper)
        {
            _sizeDAL = sizeDAL;
            _validationRules = validationRules;
            _mapper = mapper;
        }
        public IDataResult<List<string>> Add(AddToSizeDTO addToSizeDTO)
        {
            Size size = _mapper.Map<Size>(addToSizeDTO);
            var result = _validationRules.Validate(size);
            if (!result.IsValid)
            {
                return new ErrorDataResult<List<string>>(result.Errors.Select(x => x.PropertyName).ToList(), result.Errors.Select(x => x.ErrorMessage).ToList());
            }
            _sizeDAL.Add(size);
            return new SuccessDataResult<List<string>>(null, CommonOperationMessages.DataAddedSuccessfully);
        }

        public IResult Delete(Size size)
        {
            _sizeDAL.Update(size);
            return new SuccessResult(CommonOperationMessages.DataDeletedSuccessfully);
        }

        public IDataResult<List<ListToSizeDTO>> GetAll()
        {
            List<Size> sizeList = _sizeDAL.GetAll(x => x.Deleted == 0);
            sizeList.Sort((x, y) => y.ID.CompareTo(x.ID));
            return new SuccessDataResult<List<ListToSizeDTO>>(_mapper.Map<List<ListToSizeDTO>>(sizeList));
        }

        public IDataResult<Size> GetById(int id)
        {
            return new SuccessDataResult<Size>(_sizeDAL.GetSize(x => x.ID == id && x.Deleted == 0));
        }

        public IDataResult<List<string>> Update(UpdateToSizeDTO updateToSizeDTO)
        {
            Size size = _mapper.Map<Size>(updateToSizeDTO);
            var result = _validationRules.Validate(size);
            if (!result.IsValid)
            {
                return new ErrorDataResult<List<string>>(result.Errors.Select(x => x.PropertyName).ToList(), result.Errors.Select(x => x.ErrorMessage).ToList());
            }
            _sizeDAL.Update(size);
            return new SuccessDataResult<List<string>>(null, CommonOperationMessages.DataUpdatedSuccessfully);
        }
    }
}
