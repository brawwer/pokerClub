using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokerClub.Models
{
    public class StatsView
    {
        public string MemberName { get; set; }
        public double TotalBuyin { get; set; }
        public double TotalRebuy { get; set; }
        public double TotalAddon { get; set; }
        public double TotalPrizes { get; set; }
        public double TotalNetAmt { get; set; }
    }

    public class StatsViewModel
    {
        public List <StatsView> StatsViews { get; set; }
    }
}

