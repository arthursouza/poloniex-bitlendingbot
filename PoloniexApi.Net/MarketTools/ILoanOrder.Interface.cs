namespace Jojatekok.PoloniexAPI.MarketTools
{
    public interface ILoanOrder
    {
        double Rate { get; }
        double Amount { get; }
        double RangeMin { get; }
        double RangeMax { get; }
    }
}
