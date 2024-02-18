namespace AuctionSite.API.Contracts
{
    public class ErrorModel
    {
        public string? PropertyName { get; set; }
        public List<string> Descriptions { get; set; } = new List<string>();
    }
}
