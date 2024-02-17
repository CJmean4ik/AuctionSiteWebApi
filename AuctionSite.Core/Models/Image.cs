using CSharpFunctionalExtensions;

namespace AuctionSite.Core.Models
{
    public class Image
    {
        public int Id { get; }
        public string Name { get; } = string.Empty;

        private Image(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public static Result<Image> Create(string name, int id = 0)
                   => Result.Success(new Image(id, name));

    }
}
