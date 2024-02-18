using AuctionSite.DataAccess.Components.UpdateComponents;

namespace AuctionSite.Core.Models
{
    public class LotEntity
    {
        [IgnoreDuringSelectionAttribute]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;

        [IgnoreDuringSelectionAttribute]
        public string ImagePreview { get; set; } = string.Empty;

    }
}
