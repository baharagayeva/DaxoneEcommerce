using Core.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.TableModels
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryID { get; set; }
        public bool IsSale { get; set; }
        public int Price { get; set; }
        public int SalePrice { get; set; }
        public string ImgPath { get; set; }
        public string Model { get; set; }
        public int StockCount { get; set; }

        public Category Category { get; set; }
        public ICollection<ProductColor> ProductColors { get; set; }= new List<ProductColor>();
        public ICollection<ProductSize> ProductSizes { get; set; } = new List<ProductSize>();
        public ICollection<ProductProductStatus> ProductProductStatuses { get; set; } = new List<ProductProductStatus>();

        //public List<ProductStatus> ProductStatuses { get; set; }
        //public List<Color> Colors { get; set; }
        //public List<Size> Sizes { get; set; }
    }
}
