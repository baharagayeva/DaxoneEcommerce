using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.TableModels
{
    public class Color : BaseEntity
    {
        public string Name { get; set; }
        public string ColorCode { get; set; }
        public ICollection<ProductColor> ProductColors { get; set; } = new List<ProductColor>();
    }
}
