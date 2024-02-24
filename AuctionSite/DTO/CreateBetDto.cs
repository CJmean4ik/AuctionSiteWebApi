namespace AuctionSite.API.DTO
{
    public class CreateBetDto
    {
        public int LotId { get; set; }
        public string Comments { get; set; }
        public decimal Price { get; set; }
    }
}
