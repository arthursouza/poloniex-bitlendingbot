using Newtonsoft.Json;
using System.Collections.Generic;

namespace Jojatekok.PoloniexAPI.WalletTools
{
    public class DepositWithdrawalList
    {
        [JsonProperty("deposits")]
        public List<Deposit> Deposits { get; private set; }

        [JsonProperty("withdrawals")]
        public List<Withdrawal> Withdrawals { get; private set; }
    }
}
