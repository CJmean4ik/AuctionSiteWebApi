using AuctionSite.API.DTO;
using AuctionSite.DataAccess.Entities;
using AuctionSite.Core.Models;
using AutoMapper;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Routing.Constraints;

namespace AuctionSite.API.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CreateLotDto, SpecificLot>()
                .ConstructUsing(entity => SpecificLot
                .Create(Lot
                    .Create(entity.Name,
                            entity.ShortDescription,
                            entity.CategoryName,
                            Image.Create(entity.ImagePreviewName, 5000).Value, 0,entity.BuyerId).Value,
                    entity.FullDescription,
                    entity.StartDate,
                    DateTime.Now.AddDays((double)entity.DurationSale!),
                    entity.DurationSale,
                   0,
                   entity.LotStatus.ToString()).Value);

            CreateMap<UpdateLotDto, SpecificLot>()
           .ConstructUsing(entity => SpecificLot.Create(Lot.Create(entity.Name, entity.ShortDescription, entity.CategoryName, null, entity.Id, null).Value,
                                                      entity.FullDescription,
                                                      null,
                                                      null,
                                                      entity.DurationSale,
                                                      0,
                                                      entity.LotStatus == null ? "" : entity.LotStatus.ToString()).Value);

            CreateMap<SpecificLot, SpecificLotEntity>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(m => m.Lot!.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(m => m.Lot!.Name))
                .ForMember(dest => dest.ShortDescription, opt => opt.MapFrom(m => m.Lot.ShortDescription))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(m => m.Lot.CategoryName))
                .ForMember(dest => dest.ImagePreview, opt => opt.MapFrom(m => m.Lot.ImagePreview!.Name))
                .ForMember(dest => dest.WhoCreatedUserId, opt => opt.MapFrom(m => m.Lot.BuyerId));

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
                                                   Image.Create(entity.ImagePreview, 5000).Value : null, entity.Id,null).Value);

            CreateMap<SpecificLotEntity, SpecificLot>()
            .ConstructUsing(entity => SpecificLot.Create(Lot.Create(entity.Name, entity.ShortDescription, entity.CategoryName, Image.Create(entity.ImagePreview, 5000).Value, entity.Id, entity.WhoCreatedUserId).Value,
                                              entity.FullDescription,
                                              entity.StartDate,
                                              entity.EndDate,
                                              entity.DurationSale,
                                             entity.MaxPrice,
                                             entity.LotStatus.ToString()).Value)
            .ForMember(dest => dest.Bets, opt => opt.MapFrom(src => src.Bets!.Select(s => MapBet(s))));

            CreateMap<BuyerEntity, Buyer>()
                .ConstructUsing(entity => MapUser(entity));

            CreateMap<BetEntity, Bet>()
               .ConstructUsing(entity => MapBet(entity));

            CreateMap<Buyer, BuyerEntity>()
             .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
             .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.User.Password))
             .ForMember(dest => dest.PasswordSalt, opt => opt.MapFrom(src => src.User.PasswordSalt))
             .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.User.Id))
             .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.User.Role))
             .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
             .ForMember(dest => dest.SecondName, opt => opt.MapFrom(src => src.SecondName));


        }
        private Buyer MapUser(BuyerEntity buyerEntity)
        {
            var user = User.Create(buyerEntity.Email, buyerEntity.Role, buyerEntity.Id).Value;
            user.SetPassword(buyerEntity.Password);
            user.SetSalt(buyerEntity.PasswordSalt);

            var buyer = Buyer.Create(buyerEntity.FirstName,buyerEntity.SecondName,user, buyerEntity.Id).Value;
            return buyer;
        }
        private Bet MapBet(BetEntity betEntity)
        {
            if (betEntity == null)
                return null;

            var buyer = betEntity.Buyer;
            if (buyer == null)
                return null;

            string comments = betEntity.Comments?.Text;

            var bet = Bet.Create(buyer.FirstName, buyer.SecondName, comments, betEntity.Price, betEntity.Id).Value;

            if (betEntity.Comments != null && betEntity.Comments.ReplyComments != null)
            {
                var replyComments = betEntity.Comments.ReplyComments.Select(c =>
                    ReplyComments.Create(c.Text, c.UserName).Value).ToList();
                bet.AddReplyComments(replyComments);
            }

            return bet;
        }
    }
}
