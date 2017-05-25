using System;
using Newtonsoft.Json;

namespace Jojatekok.PoloniexAPI.WalletTools
{
    public class ActiveLoan
    {
        /*
        {
            "provided":
            [
                {
                    "id":75073,"currency":"LTC","rate":"0.00020000","amount":"0.72234880","range":2,"autoRenew":0,"date":"2015-05-10 23:45:05","fees":"0.00006000"
                },
                {
                    "id":74961,"currency":"LTC","rate":"0.00002000","amount":"4.43860711","range":2,"autoRenew":0,"date":"2015-05-10 23:45:05","fees":"0.00006000"
                }
            ],
            "used":
            [
                {
                    "id":75238,
                    "currency":"BTC",
                    "rate":"0.00020000",
                    "amount":"0.04843834",
                    "range":2,
                    "date":"2015-05-10 23:51:12",
                    "fees":"-0.00000001"
                }
            ]
        }
        */

        [JsonProperty("id")]
        public int Id { get; private set; }

        [JsonProperty("currency")]
        public string Currency { get; private set; }

        [JsonProperty("rate")]
        public double Rate { get; private set; }

        [JsonProperty("amount")]
        public double Amount { get; private set; }

        [JsonProperty("range")]
        public double Range { get; private set; }

        [JsonProperty("autoRenew")]
        public bool AutoRenew { get; private set; }

        [JsonProperty("date")]
        public DateTime Date { get; private set; }
        
        [JsonProperty("fees")]
        public double Fees{ get; private set; }
    }
}
