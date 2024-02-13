using CSharpFunctionalExtensions;

namespace AuctionSite.Core.Models
{
    public class Image
    {
        public Guid Id { get; }
        public string Name { get; } = string.Empty;

        private Image(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
        public static Result<Image> Create(Guid id, string name)
        {
            var image = new Image(id, name);

            return Result.Success(image);
        }
    }
}
