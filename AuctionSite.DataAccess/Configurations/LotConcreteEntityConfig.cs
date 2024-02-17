using AuctionSite.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuctionSite.DataAccess.Configurations
{
    internal class LotConcreteEntityConfig : IEntityTypeConfiguration<LotConcreteEntity>
    {
        public void Configure(EntityTypeBuilder<LotConcreteEntity> builder)
        {
            builder.Property(p => p.LotStatus)
                   .HasConversion<string>();
        }
    }
}
