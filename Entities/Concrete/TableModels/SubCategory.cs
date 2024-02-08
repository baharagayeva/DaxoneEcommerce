using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.TableModels
{
    public class SubCategory : BaseEntity
    {
        public string Name { get; set; }
        public int CategoryID { get; set; }

        public Category Category { get; set; }
    }
}
