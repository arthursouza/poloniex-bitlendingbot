using Newtonsoft.Json;

namespace Jojatekok.PoloniexAPI.WalletTools
{
    public class OpenLoanOffer
    {
        /*
        {"BTC":
        [
            {
                "id":10595,
                "rate":"0.00020000",
                "amount":"3.00000000",
                "duration":2,
                "autoRenew":1,
                "date":"2015-05-10 23:33:50"
            }
        ]
        */

        [JsonProperty("id")]
        public int Id { get; private set; }
        [JsonProperty("rate")]
        public double Double { get; private set; }
        [JsonProperty("amount")]
        public double Amount { get; private set; }
        [JsonProperty("duration")]
        public double Duration { get; private set; }
        [JsonProperty("autoRenew")]
        public bool AutoRenew { get; private set; }
    }
}
