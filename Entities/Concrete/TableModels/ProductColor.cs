using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.TableModels
{
    public class ProductColor
    {
        public int ID { get; set; }
        public int? ProductID { get; set; }
        public Product? Product { get; set; }
        public int? ColorID { get; set; }
        public Color? Color { get; set; }

    }
}
