using AutoMapper;
using eCommerce.Core.DTO;
using eCommerce.Core.Entities;

namespace eCommerce.Core.Mapper
{
    public class ApplicationUserMappingProfile : Profile
    {
        public ApplicationUserMappingProfile()
        {
            CreateMap<ApplicationUser, AuthenticationResponse>()
            .ConstructUsing(user => new AuthenticationResponse(
                user.UserID,
                user.Email,
                user.PersonName,
                user.Gender,
                "default-token",
                true
            ));

            CreateMap<RegistorRequestDTO, ApplicationUser>().ReverseMap();
            
        }
    }
}
