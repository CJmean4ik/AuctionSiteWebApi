using AuctionSite.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuctionSite.DataAccess.Configurations
{
    internal class BetEntityConfig : IEntityTypeConfiguration<BetEntity>
    {
        public void Configure(EntityTypeBuilder<BetEntity> builder)
        {
            builder.HasKey(hk => hk.Id);

            builder.HasOne(ho => ho.Comments)
                   .WithOne(wo => wo.Bet)
                   .HasForeignKey<CommentsEntity>(fk => fk.BetId);
        }
    }
}
