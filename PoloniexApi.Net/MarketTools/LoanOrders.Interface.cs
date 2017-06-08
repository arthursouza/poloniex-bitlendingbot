using System.Collections.Generic;

namespace Jojatekok.PoloniexAPI.MarketTools
{
    public interface ILoanOrders
    {
        IList<LoanOrder> Demands { get; }
        IList<LoanOrder> Offers { get; }
    }
}
