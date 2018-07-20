using AutoMapper;
using Training.Models;
using Training.ViewModels;

namespace Training.AutoMapperProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductViewModel>().ReverseMap();
        }
    }
}
