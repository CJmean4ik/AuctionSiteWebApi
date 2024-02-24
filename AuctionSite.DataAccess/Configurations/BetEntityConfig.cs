using AuctionSite.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuctionSite.DataAccess.Configurations
{
    internal class BetEntityConfig : IEntityTypeConfiguration<BetEntity>
    {
        public void Configure(EntityTypeBuilder<BetEntity> builder)
        {
            builder.HasKey(hk => hk.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            /*
            builder.HasOne(ho => ho.ReplyComments)
                   .WithOne(wo => wo.)
                   .HasForeignKey<CommentsEntity>(fk => fk.BetId);
            */
        }
    }
}
