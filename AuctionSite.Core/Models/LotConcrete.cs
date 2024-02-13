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
        public DateTime HaveTime { get; }
        public Image FullImage { get; }
        public decimal MaxPrice { get; }
        public bool IsSold { get; }

        public IReadOnlyCollection<Bet> Bets => _bets;

        public void AddBets(List<Bet> bets) => bets.AddRange(bets);

        public LotConcrete(Lot lot,
                           string fullDescription,
                           DateTime startDate,
                           DateTime endDate,
                           DateTime haveTime,
                           Image fullImage,
                           decimal maxPrice,
                           bool isSold)
        {
            Lot = lot;
            FullDescription = fullDescription;
            StartDate = startDate;
            EndDate = endDate;
            HaveTime = haveTime;
            FullImage = fullImage;
            MaxPrice = maxPrice;
            IsSold = isSold;
        }

        public static Result<LotConcrete> Create(Lot lot,
                                                 string fullDescription,
                                                 DateTime startDate,
                                                 DateTime endDate,
                                                 DateTime haveTime,
                                                 Image fullImage,
                                                 decimal maxPrice,
                                                 bool isSold) =>
            Result.Success(new LotConcrete(lot,
                                              fullDescription,
                                              startDate,
                                              endDate,
                                              haveTime,
                                              fullImage,
                                              maxPrice,
                                              isSold));

    }
}
