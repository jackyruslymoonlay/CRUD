using AutoMapper;
using Training.Models;
using Training.ViewModels;

namespace Training.AutoMapperProfiles
{
    public class BuyerProfile : Profile
    {
        public BuyerProfile()
        {
            CreateMap<Buyer, BuyerViewModel>().ReverseMap();
        }
    }
}
