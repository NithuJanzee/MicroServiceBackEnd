using AutoMapper;
using BusinessLogicLayer.DTO;
using DataAccessLayer.Entities;

namespace BusinessLogicLayer.Mapper
{
    public class ApplicationUserMappingProfile : Profile
    {
        public ApplicationUserMappingProfile()
        {
            CreateMap<ProductAddRequest, Products>().ReverseMap();
            CreateMap<Products, Products>().ReverseMap();
            CreateMap<ProductUpdateRequest, Products>().ReverseMap();
            CreateMap<ProductResponse, Products>().ReverseMap();
        }
    }
}
