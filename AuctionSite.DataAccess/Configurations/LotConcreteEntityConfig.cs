using AuctionSite.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuctionSite.DataAccess.Configurations
{
    internal class LotConcreteEntityConfig : IEntityTypeConfiguration<LotConcreteEntity>
    {
        public void Configure(EntityTypeBuilder<LotConcreteEntity> builder)
        {
            builder.HasKey(hk => hk.Id);

            builder.HasOne(ho => ho.Lot)
                   .WithOne(wo => wo.LotConcrete)
                   .HasForeignKey<LotEntity>(fk => fk.LotConcreteId);
        }
    }
}
