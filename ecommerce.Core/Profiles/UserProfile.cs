using AutoMapper;
using ecommerce.Core.Models;


namespace ecommerce.Core.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => 
                        dest.Items, 
                    opt => 
                        opt.MapFrom(src => src.Items));

            CreateMap<UserDto, User>().ForMember(dest => 
                    dest.Items,
                opt => 
                    opt.MapFrom(src => src.Items));
        }
    }
}