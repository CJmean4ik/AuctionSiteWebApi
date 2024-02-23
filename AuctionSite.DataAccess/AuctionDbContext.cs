using AuctionSite.DataAccess.Entities;
using AuctionSite.DataAccess.Configurations;
using Microsoft.EntityFrameworkCore;

namespace AuctionSite.DataAccess
{
    public class AuctionDbContext : DbContext
    {
        public DbSet<BetEntity> Bets { get; set; }
        public DbSet<BuyerEntity> Buyers { get; set; }
        public DbSet<CommentsEntity> Comments { get; set; }
        public DbSet<SpecificLotEntity> SpecificLot { get; set; }
        public DbSet<LotEntity> Lots { get; set; }
        public DbSet<ReplyCommentsEntity> ReplyComments { get; set; }
        public DbSet<UserEntity> Users { get; set; }

        public AuctionDbContext()
        {
        }

        public AuctionDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BetEntityConfig());
            modelBuilder.ApplyConfiguration(new CommentsEntityConfig());
            modelBuilder.ApplyConfiguration(new LotEntityConfig());
            modelBuilder.ApplyConfiguration(new LotConcreteEntityConfig());
            modelBuilder.ApplyConfiguration(new UserEntityConfig());
        }
    }
}
