using AuctionSite.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuctionSite.DataAccess.Configurations
{
    internal class CommentsEntityConfig : IEntityTypeConfiguration<ReplyCommentsEntity>
    {
        public void Configure(EntityTypeBuilder<ReplyCommentsEntity> builder)
        {
            
            builder.HasKey(hk => hk.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
          
            builder.HasOne(ho => ho.Bet)
                   .WithMany(wm => wm.ReplyComments);
        }
    }
}
