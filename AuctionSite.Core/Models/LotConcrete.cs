using CSharpFunctionalExtensions;

namespace AuctionSite.Core.Models
{
    public class LotConcrete
    {
        private readonly List<Bet> _bets = new List<Bet>();

        public Lot Lot { get; }
        public string FullDescription { get; } = string.Empty;
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }
        public int DurationSale { get; }
        public Image FullImage { get; }
        public decimal MaxPrice { get; }
        public string LotStatus { get; }

        public IReadOnlyCollection<Bet> Bets => _bets;

        public void AddBets(List<Bet> bets) => bets.AddRange(bets);

        public LotConcrete(Lot lot,
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

        public static Result<LotConcrete> Create(Lot lot,
                                                 string fullDescription,
                                                 DateTime startDate,
                                                 DateTime endDate,
                                                 int durationSale,
                                                 Image fullImage,
                                                 decimal maxPrice,
                                                 string lotStatus) =>
            Result.Success(new LotConcrete(lot,
                                              fullDescription,
                                              startDate,
                                              endDate,
                                              durationSale,
                                              fullImage,
                                              maxPrice,
                                              lotStatus));

    }
}
