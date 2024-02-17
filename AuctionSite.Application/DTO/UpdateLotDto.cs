namespace AuctionSite.Application.DTO
{
    public class UpdateLotDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public string FullDescription { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }
}