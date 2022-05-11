using AutoMapper;
using ecommerce.Core.Models;

namespace ecommerce.Core.Profiles
{
    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<ItemCreateDto, Item>()
                .ForMember(dest => dest.OwnerId,
                    opt =>
                        opt.MapFrom(src => src.OwnerId))
                .ForMember(dest => dest.Type,
                    opt =>
                        opt.MapFrom(src => src.Type));
        }
    }
}