using AuctionSite.DataAccess.Components.UpdateComponents;

namespace AuctionSite.Core.Models
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

    }
}
