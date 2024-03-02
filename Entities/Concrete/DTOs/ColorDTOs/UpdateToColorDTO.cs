using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.DTOs.ColorDTOs
{
    public class UpdateToColorDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ColorCode { get; set; }
    }
}
