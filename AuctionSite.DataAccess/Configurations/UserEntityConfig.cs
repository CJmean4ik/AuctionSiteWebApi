using AuctionSite.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuctionSite.DataAccess.Configurations
{
    internal class UserEntityConfig : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasKey(hk => hk.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
        }
    }
}
