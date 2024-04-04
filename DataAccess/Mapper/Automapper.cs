using AutoMapper;
using Entities.Concrete.DTOs.CategoryDTOs;
using Entities.Concrete.DTOs.ColorDTOs;
using Entities.Concrete.DTOs.ProductDTOs;
using Entities.Concrete.DTOs.ProductStatusDTOs;
using Entities.Concrete.DTOs.SizeDTOs;
using Entities.Concrete.DTOs.SubCategoryDTOs;
using Entities.Concrete.TableModels;
using Entities.Concrete.ViewModels;
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

            CreateMap<Color, ListToColorDTO>().ReverseMap();
            CreateMap<AddToColorDTO, Color>().ReverseMap();
            CreateMap<UpdateToColorDTO, Color>().ReverseMap();

            CreateMap<Product, ListToProductDTO>().ReverseMap();
            CreateMap<AddToProductDTO, Product>().ReverseMap();
            CreateMap<UpdateToProductDTO, Product>().ReverseMap();
            CreateMap<ProductGetViewModel, Product>().ReverseMap();


            CreateMap<ProductStatus, ListToProductStatusDTO>().ReverseMap();
            CreateMap<AddToProductStatusDTO, ProductStatus>().ReverseMap();
            CreateMap<UpdateToProductStatusDTO, ProductStatus>().ReverseMap();

            CreateMap<Size, ListToSizeDTO>().ReverseMap();
            CreateMap<AddToSizeDTO, Size>().ReverseMap();
            CreateMap<UpdateToSizeDTO, Size>().ReverseMap();    

            CreateMap<SubCategory, ListToSubCategoryDTO>().ReverseMap();
            CreateMap<AddToSubCategoryDTO, SubCategory>().ReverseMap();
            CreateMap<UpdateToSubCategoryDTO, SubCategory>().ReverseMap();
        }
    }
}
