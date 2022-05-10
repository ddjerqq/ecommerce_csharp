using AutoMapper;
using ecommerce.Core.Models;

namespace ecommerce.Core.Profiles
{
    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<Item, ItemDto>()
                .ForMember(dest => dest.Type,
                    opt =>
                        opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.OwnerId,
                    opt =>
                        opt.MapFrom(src => src.OwnerId));

            CreateMap<ItemDto, Item>()
                .ForMember(dest => dest.OwnerId,
                    opt =>
                        opt.MapFrom(src => src.OwnerId))
                .ForMember(dest => dest.Type,
                    opt =>
                        opt.MapFrom(src => src.Type));
        }
    }
}