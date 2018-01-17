using Newtonsoft.Json;

namespace Jojatekok.PoloniexAPI.WalletTools
{
    public class CreateLoanOffer
    {
        [JsonProperty("autoRenew")] private int autoRenew;

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("amount")]
        public double Amount { get; set; }

        [JsonProperty("lendingRate")]
        public double LendingRate { get; set; }
    }
}