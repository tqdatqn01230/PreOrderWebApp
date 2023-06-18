using System;
using System.Collections.Generic;

namespace DataLayer.Models
{
    public partial class AccountOrder
    {
        public int AccountOrderId { get; set; }
        public int ProductCampaignId { get; set; }
        public int Unit { get; set; }
        public string? Note { get; set; }
        public int? AccountId { get; set; }

        public virtual Account? Account { get; set; }
        public virtual ProductCampaign ProductCampaign { get; set; } = null!;
    }
}
