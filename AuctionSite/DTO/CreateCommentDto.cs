namespace AuctionSite.API.DTO
{
    public class CreateCommentDto
    {
        public int BetId { get; set; }
        public string Text { get; set; }
        public string? UserName { get; set; } = "Anonymous";

    }
}
