using Core.Helpers.Results.Abstract;
using Entities.Concrete.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductStatusService
    {
        IDataResult<List<string>> Add(ProductStatus productStatus);
        IResult Delete(ProductStatus productStatus);
        IDataResult<List<string>> Update(ProductStatus productStatus);
        IDataResult<ProductStatus> GetById(int id);
        IDataResult<List<ProductStatus>> GetAll();
    }
}
