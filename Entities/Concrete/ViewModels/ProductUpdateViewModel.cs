using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.ViewModels
{
    public class ProductUpdateViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int[] SizeIds { get; set; }
        public int[] ColorIds { get; set; }
        public int[] ProductStatusIds { get; set; }
        public string Description { get; set; }
        public int CategoryID { get; set; }
        public bool IsSale { get; set; }
        public int Price { get; set; }
        public int SalePrice { get; set; }
        public string ImgPath { get; set; }
        public string Model { get; set; }
        public int StockCount { get; set; }
        public IFormFile Image { get; set; }
    }
}
