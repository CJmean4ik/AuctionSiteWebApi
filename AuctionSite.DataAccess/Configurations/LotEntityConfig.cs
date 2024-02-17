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

            builder.Property(p => p.Id).ValueGeneratedOnAdd();
        }
    }
}
