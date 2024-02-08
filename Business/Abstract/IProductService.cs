using Core.Helpers.Results.Abstract;
using Entities.Concrete.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductService
    {
        IDataResult<List<string>> Add(Product product);
        IResult Delete(Product product);
        IDataResult<List<string>> Update(Product product);
        IDataResult<Product> GetById(int id);
        IDataResult<List<Product>> GetAll();
    }
}
