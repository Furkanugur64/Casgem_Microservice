using AutoMapper;
using CasgemMicroservices.Catalog.DTOs.CategoryDTOs;
using CasgemMicroservices.Catalog.DTOs.ProductDTOs;
using CasgemMicroservices.Catalog.Models;

namespace CasgemMicroservices.Catalog.Mapping
{
    public class GeneralMapping: Profile
    {
        public GeneralMapping()
        {
            CreateMap<Category,ResultCategoryDTO>().ReverseMap();
            CreateMap<Category,CreateCategoryDTO>().ReverseMap();
            CreateMap<Category,UpdateCategoryDTO>().ReverseMap();
            CreateMap<Product,ResultProductDTO>().ReverseMap();
            CreateMap<Product,CreateProductDTO>().ReverseMap();
            CreateMap<Product,UpdateProductDTO>().ReverseMap();
        }
    }
}
