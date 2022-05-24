using AutoMapper;
using ecommerce.Core.Models;


namespace ecommerce.Core.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            // CreateMap<UserCreateDto, User>()
            //     .ForMember(dest => dest.Username,
            //     opt => 
            //         opt.MapFrom(src => src.Username));
        }
    }
}