using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Jojatekok.PoloniexAPI.MarketTools
{
    public class LoanOrders : ILoanOrders
    {
        [JsonProperty("demands")]
        public IList<LoanOrder> Demands { get; set; }

        [JsonProperty("offers")]
        public IList<LoanOrder> Offers { get; set; }
    }
}
