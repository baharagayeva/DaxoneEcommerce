using Entities.Concrete.DTOs.ColorDTOs;
using Entities.Concrete.DTOs.ProductStatusDTOs;
using Entities.Concrete.DTOs.SizeDTOs;
using Entities.Concrete.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.DTOs.ProductDTOs
{
    public class ListToProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryID { get; set; }
        public bool IsSale { get; set; }
        public int Price { get; set; }
        public int SalePrice { get; set; }
        public string ImgPath { get; set; }
        public string Model { get; set; }
        public int StockCount { get; set; }

        public List<ListToColorDTO> Colors { get; set; }
        public List<ListToSizeDTO> Sizes { get; set; }
        public List<ListToProductStatusDTO> ProductStatuses { get; set; }
    }
}
