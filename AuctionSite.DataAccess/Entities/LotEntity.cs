using AuctionSite.DataAccess.Components.UpdateComponents;

namespace AuctionSite.DataAccess.Entities
{
    public class LotEntity
    {
        [IgnoreDuringSelectionAttribute]
        public int Id { get; set; }

        public string? Name { get; set; }
        public string? ShortDescription { get; set; }
        public string? CategoryName { get; set; }

        [IgnoreDuringSelectionAttribute]
        public string ImagePreview { get; set; } = string.Empty;

        [IgnoreDuringSelectionAttribute]
        public int? WhoCreatedUserId { get; set; }

        [IgnoreDuringSelectionAttribute]
        public BuyerEntity? Entity { get; set; }
    }
}
