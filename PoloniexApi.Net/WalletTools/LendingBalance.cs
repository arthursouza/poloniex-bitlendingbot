using Newtonsoft.Json;

namespace Jojatekok.PoloniexAPI.WalletTools
{
    public class LendingBalance
    {
        [JsonProperty("BTC")]
        public double? Btc { get; set; }
    }
}