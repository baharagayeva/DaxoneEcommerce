using Core.Helpers.Results.Abstract;
using Entities.Concrete.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ISeasonDiscountService
    {
        IDataResult<List<string>> Add(SeasonDiscount seasonDiscount);
        IResult Delete(SeasonDiscount seasonDiscount);
        IDataResult<List<string>> Update(SeasonDiscount seasonDiscount);
        IDataResult<SeasonDiscount> GetById(int id);
        IDataResult<List<SeasonDiscount>> GetAll();
    }
}
