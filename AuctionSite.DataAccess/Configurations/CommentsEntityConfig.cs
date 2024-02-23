using AuctionSite.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuctionSite.DataAccess.Configurations
{
    internal class CommentsEntityConfig : IEntityTypeConfiguration<CommentsEntity>
    {
        public void Configure(EntityTypeBuilder<CommentsEntity> builder)
        {
            builder.HasKey(hk => hk.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.HasOne(ho => ho.Bet)
                   .WithOne(wo => wo.Comments)
                   .HasForeignKey<BetEntity>(fk => fk.CommentsId);
        }
    }
}
