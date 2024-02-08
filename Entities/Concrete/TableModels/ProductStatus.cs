using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.TableModels
{
    public class ProductStatus : BaseEntity
    {
        public string New { get; set; }
        public string InStock { get; set; }
        public string StockOut { get; set; }

        public List<Product> Products { get; set; }
    }
}
