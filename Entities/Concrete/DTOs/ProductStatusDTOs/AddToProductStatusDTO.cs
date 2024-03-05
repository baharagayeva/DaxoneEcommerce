using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.DTOs.ProductStatusDTOs
{
    public class AddToProductStatusDTO
    {
        public string New { get; set; }
        public string InStock { get; set; }
        public string StockOut { get; set; }
    }
}
