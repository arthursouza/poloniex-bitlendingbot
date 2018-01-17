using System.Collections.Generic;
using Newtonsoft.Json;

namespace Jojatekok.PoloniexAPI.MarketTools
{
    public class LoanOrders
    {
        [JsonProperty("demands")]
        public List<LoanOrder> Demands { get; set; }

        [JsonProperty("offers")]
        public List<LoanOrder> Offers { get; set; }
    }
}