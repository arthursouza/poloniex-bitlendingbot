using System;

namespace Jojatekok.PoloniexAPI.MarketTools
{
    public class LoanOrder
    {
        internal LoanOrder()
        {

        }

        public DateTime Date { get; set; }
        public double Rate { get; set; }
        public double Amount { get; set; }
        public double RangeMin { get; set; }
        public double RangeMax { get; set; }
    }
}
