using Core.Helpers.Results.Abstract;
using Entities.Concrete.DTOs.ColorDTOs;
using Entities.Concrete.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IColorService
    {
        IDataResult<List<string>> Add(AddToColorDTO addToColorDTO);
        IResult Delete(Color color);
        IDataResult<List<string>> Update(UpdateToColorDTO updateToColorDTO);
        IDataResult<Color> GetById(int id);
        IDataResult<List<ListToColorDTO>> GetAll();
    }
}
