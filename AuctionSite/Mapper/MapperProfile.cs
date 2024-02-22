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
                                                Image.Create(entity.ImagePreview.FileName, 5000).Value : null, 0).Value);

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

            CreateMap<SpecificLotEntity, SpecificLot>()
            .ConstructUsing(entity => SpecificLot.Create(null,
                                              entity.FullDescription,
                                              entity.StartDate,
                                              entity.EndDate,
                                              entity.DurationSale,
                                              string.IsNullOrEmpty(entity.FullImage) ? null : Image.Create(entity.FullImage, 5000).Value,
                                             entity.MaxPrice,
                                             entity.LotStatus.ToString()).Value)
            .ForMember(dest => dest.Bets, opt => opt.MapFrom(src => MapBets(src.Bets)));           
        }
        private List<Bet?> MapBets(List<BetEntity>? bets)
        {
            if (bets == null)
                return new List<Bet?>();

            return bets.Select(s =>
            {
                var buyer = s.Buyer;
                if (buyer == null)
                    return null;


                var bet = Bet.Create(s.Price, buyer.FirstName, buyer.SecondName, s.Comments?.Text).Value;

                if (s.Comments != null && s.Comments.ReplyComments != null)
                {
                    var replyComments = s.Comments.ReplyComments.Select(c =>
                        ReplyComments.Create(c.Text, c.UserName).Value).ToList();
                    bet.AddReplyComments(replyComments);
                }

                return bet;
            }).Where(b => b != null).ToList();
        }
    }
}
