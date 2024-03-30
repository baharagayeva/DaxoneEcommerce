using Core.DataAccess.Abstract;
using Entities.Concrete.TableModels;
using Entities.Concrete.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IProductDAL : IRepository<Product>
    {
        public List<Product> GetAllWithProduct();
        public Product GetProductById(int id);

        public void AddWithProduct(ProductGetViewModel producSizeGetViewModel);
        public void UpdateWithProduct(ProductUpdateViewModel producSizeGetViewModel);
    }
}
