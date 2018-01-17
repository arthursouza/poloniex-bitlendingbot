using System.Collections.Generic;
using Newtonsoft.Json;

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