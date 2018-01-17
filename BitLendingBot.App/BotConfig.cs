using System;
using Newtonsoft.Json;

namespace BitLendingBot.App
{
    public class BotConfig
    {
        public BotConfig()
        {
            BotFilesFolder = @"C:\lendingbot";
            BaseLogFile = @"C:\lendingbot\bitbotlog-{0}.json";
            MyLoanHistory = @"C:\lendingbot\my_loan_history.json";
            PublicLoanOfferHistory = @"C:\lendingbot\public_loan_offer_history.json";

            IntervalBetweenLoops = 2 * 60 * 1000;
            IntervalBetweenCalls = 15000;
            PercentageToLend = 0.5;
        }

        public string BotFilesFolder { get; set; }
        public string BaseLogFile { get; set; }
        public int IntervalBetweenLoops { get; set; }
        public int IntervalBetweenCalls { get; set; }
        public double PercentageToLend { get; set; }
        public string MyLoanHistory { get; set; }
        public string PublicLoanOfferHistory { get; set; }

        [JsonIgnore]
        public string CurrentLogFile => string.Format(BaseLogFile, DateTime.Now.Ticks);
    }
}