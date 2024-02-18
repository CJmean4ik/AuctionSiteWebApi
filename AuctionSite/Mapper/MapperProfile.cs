using AuctionSite.API.DTO;
using AuctionSite.Core.Models;
using AutoMapper;

namespace AuctionSite.API.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CreateLotDto, Lot>()
                .ConstructUsing(entity => Lot.Create(entity.Name,
                                                entity.ShortDescription,
                                                entity.CategoryName,
                                                !string.IsNullOrEmpty(entity.ImagePreview.FileName) ?
                                                Image.Create(entity.ImagePreview.FileName, 5000).Value : null,0).Value);

            CreateMap<UpdateLotDto, Lot>()
           .ConstructUsing(entity => Lot.Create(entity.Name,
                                                entity.ShortDescription,
                                                 entity.CategoryName,
                                                 null,
                                                 entity.Id).Value);

            CreateMap<Lot, LotEntity>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(map => map.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(map => map.Name))
                .ForMember(dest => dest.ShortDescription, opt => opt.MapFrom(map => map.ShortDescription))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(map => map.CategoryName))
                .ForMember(dest => dest.ImagePreview, opt => opt.MapFrom(map => map.ImagePreview == null ? "" : map.ImagePreview.Name));

            CreateMap<LotEntity, Lot>()
                   .ConstructUsing(entity => Lot.Create(entity.Name,
                                                   entity.ShortDescription,
                                                   entity.CategoryName,
                                                   !string.IsNullOrEmpty(entity.ImagePreview) ?
                                                   Image.Create(entity.ImagePreview, 5000).Value : null, entity.Id).Value);

        }
    }
}
