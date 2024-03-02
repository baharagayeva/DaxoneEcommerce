using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Entities.Concrete.TableModels
{
    public class AdvertisementBanner : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImgPath { get; set; }
        public int Discount { get; set; }

    }
}
