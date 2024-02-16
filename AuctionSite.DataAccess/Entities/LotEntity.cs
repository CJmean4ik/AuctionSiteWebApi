namespace AuctionSite.Core.Models
{
    public class LotEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public string ImagePreview { get; set; } = string.Empty;

        public Guid LotConcreteId { get; set; }
        public LotConcreteEntity? LotConcrete { get; set; }
    }
}
