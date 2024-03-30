using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.TableModels
{
    public class ProductProductStatus
    {
        public int ID { get; set; }
        public int? ProductID { get; set; }
        public Product? Product { get; set; }
        public int? ProductStatusID { get; set; }
        public ProductStatus? ProductStatus { get; set; }

    }
}
