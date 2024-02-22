using AuctionSite.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuctionSite.DataAccess.Configurations
{
    internal class LotConcreteEntityConfig : IEntityTypeConfiguration<SpecificLotEntity>
    {
        public void Configure(EntityTypeBuilder<SpecificLotEntity> builder)
        {
            builder.Property(p => p.LotStatus)
                   .HasConversion<string>();
        }
    }
}
