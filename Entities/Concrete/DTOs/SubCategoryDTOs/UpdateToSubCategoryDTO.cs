﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.DTOs.SubCategoryDTOs
{
    public class UpdateToSubCategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryID { get; set; }
    }
}
