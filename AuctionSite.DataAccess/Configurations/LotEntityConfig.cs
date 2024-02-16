using AuctionSite.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuctionSite.DataAccess.Configurations
{
    internal class LotEntityConfig : IEntityTypeConfiguration<LotEntity>
    {
        public void Configure(EntityTypeBuilder<LotEntity> builder)
        {
            builder.HasKey(hk => hk.Id);

            builder.HasOne(ho => ho.LotConcrete)
                   .WithOne(wo => wo.Lot)
                   .HasForeignKey<LotConcreteEntity>(fk => fk.LotId);
        }
    }
    internal class UserEntityConfig : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasKey(hk => hk.Id);

            builder.HasOne(ho => ho.Buyer)
                   .WithOne(wo => wo.User)
                   .HasForeignKey<BuyerEntity>(fk => fk.UserId);
        }
    }
}
