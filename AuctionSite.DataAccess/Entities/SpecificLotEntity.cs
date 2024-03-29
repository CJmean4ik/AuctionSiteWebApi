﻿using AuctionSite.DataAccess.Components.UpdateComponents;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionSite.DataAccess.Entities
{
    [Table("LotConcrete")]
    public class SpecificLotEntity : LotEntity
    {
        public string? FullDescription { get; set; } = string.Empty;

        [IgnoreDuringSelectionAttribute]
        public DateTime StartDate { get; set; }

        [IgnoreDuringSelectionAttribute]
        public decimal MaxPrice { get; set; }

        [IgnoreDuringSelectionAttribute]
        public DateTime? EndDate { get; set; }

        public int? DurationSale { get; set; }
        public LotStatus? LotStatus { get; set; }

        [IgnoreDuringSelectionAttribute]
        public List<BetEntity>? Bets { get; set; }
    }

    public enum LotStatus
    {
        Closed,
        Sold,
        Active
    }
}
