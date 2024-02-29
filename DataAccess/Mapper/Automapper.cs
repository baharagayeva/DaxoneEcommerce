using AutoMapper;
using Entities.Concrete.DTOs.CategoryDTOs;
using Entities.Concrete.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapper
{
    public class Automapper : Profile
    {
        public Automapper()
        {
            CreateMap<Category, ListToCategoryDTO>().ReverseMap();
            CreateMap<AddToCategoryDTO, Category>().ReverseMap();
            CreateMap<UpdateToCategoryDTO, Category>().ReverseMap();
        }
    }
}
