using AutoMapper;
using ecommerce.Core.Models;


namespace ecommerce.Core.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserCreateDto, User>()
                .ForMember(dest => dest.Username,
                opt => 
                    opt.MapFrom(src => src.Username));
        }
    }
}