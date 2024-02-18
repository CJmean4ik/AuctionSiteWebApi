using CSharpFunctionalExtensions;

namespace AuctionSite.Core.Models
{
    public class Lot
    {
        public int? Id { get; }
        public string? Name { get; } = string.Empty;
        public string? ShortDescription { get; } = string.Empty;
        public string? CategoryName { get; } = string.Empty;
        public Image? ImagePreview { get; }

        private Lot(int? lotId, string? name, string? shortDescription, string? categoryName, Image? imagePreview)
        {
            Id = lotId;
            Name = name;
            ShortDescription = shortDescription;
            CategoryName = categoryName;
            ImagePreview = imagePreview;
        }

        public static Result<Lot> Create(string? name, string? shortDescription, string? categoryName, Image? imagePreview, int? lotId = 0)
        {
            return Result.Success(new Lot(lotId, name, shortDescription, categoryName, imagePreview));
        }

    }
}
