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
        public bool IsNew { get; set; }
        public bool IsStock { get; set; }
        public bool IsStockOut { get; set; }

        public ICollection<ProductProductStatus> ProductProductStatuses { get; set; } = new List<ProductProductStatus>();
    }
}
