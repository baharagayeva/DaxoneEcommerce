using Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.TableModels
{
    public class SeasonDiscount : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string TitleDescription { get; set; }
        public string ImgPath { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
    }
}
