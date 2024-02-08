using Core.Helpers.Results.Abstract;
using Entities.Concrete.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ISizeService
    {
        IDataResult<List<string>> Add(Size size);
        IResult Delete(Size size);
        IDataResult<List<string>> Update(Size size);
        IDataResult<Size> GetById(int id);
        IDataResult<List<Size>> GetAll();
    }
}
