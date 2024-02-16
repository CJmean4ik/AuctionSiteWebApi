using AuctionSite.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuctionSite.DataAccess.Configurations
{
    internal class BuyerEntityConfig : IEntityTypeConfiguration<BuyerEntity>
    {
        public void Configure(EntityTypeBuilder<BuyerEntity> builder)
        {
            builder.HasKey(hk => hk.Id);

            builder.HasOne(ho => ho.User)
                   .WithOne(wo => wo.Buyer)
                   .HasForeignKey<UserEntity>(fk => fk.BuyerId);
        }
    }
}
