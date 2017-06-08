namespace Jojatekok.PoloniexAPI.MarketTools
{
    public class LoanOrder : ILoanOrder
    {
        internal LoanOrder()
        {

        }

        public double Rate { get; set; }
        public double Amount { get; set; }
        public double RangeMin { get; set; }
        public double RangeMax { get; set; }
    }
}
