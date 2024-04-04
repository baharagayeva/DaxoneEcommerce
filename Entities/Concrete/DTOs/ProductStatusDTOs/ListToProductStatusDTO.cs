using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.DTOs.ProductStatusDTOs
{
    public class ListToProductStatusDTO
    {
        public int Id { get; set; }
        public bool IsNew { get; set; }
        public bool IsStock { get; set; }
        public bool IsStockOut { get; set; }
    }
}
