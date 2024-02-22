using CSharpFunctionalExtensions;

namespace AuctionSite.Core.Models
{
    public class SpecificLot
    {
        private readonly List<Bet> _bets = new List<Bet>();

        public Lot? Lot { get; }
        public string FullDescription { get; } = string.Empty;
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }
        public int DurationSale { get; }
        public Image FullImage { get; }
        public decimal MaxPrice { get; }
        public string LotStatus { get; }

        public IReadOnlyCollection<Bet> Bets => _bets;

        public void AddBets(List<Bet> bets) => _bets.AddRange(bets);

        private SpecificLot(Lot? lot,
                           string fullDescription,
                           DateTime startDate,
                           DateTime endDate,
                           int durationSale,
                           Image fullImage,
                           decimal maxPrice,
                           string lotStatus)
        {
            Lot = lot;
            FullDescription = fullDescription;
            StartDate = startDate;
            EndDate = endDate;
            DurationSale = durationSale;
            FullImage = fullImage;
            MaxPrice = maxPrice;
            LotStatus = lotStatus;
        }

        public static Result<SpecificLot> Create(Lot? lot,
                                                 string fullDescription,
                                                 DateTime startDate,
                                                 DateTime endDate,
                                                 int durationSale,
                                                 Image fullImage,
                                                 decimal maxPrice,
                                                 string lotStatus) =>
            Result.Success(new SpecificLot(lot,
                                              fullDescription,
                                              startDate,
                                              endDate,
                                              durationSale,
                                              fullImage,
                                              maxPrice,
                                              lotStatus));

    }
}
