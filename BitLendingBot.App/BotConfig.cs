using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BitLendingBot.App
{
    public class BotConfig
    {
        public string BotFilesFolder { get; set; }
        public string BaseLogFile { get; set; }
        public int IntervalBetweenLoops { get; set; }
        public int IntervalBetweenCalls { get; set; }
        public double PercentageToLend { get; set; }
        public string MyLoanHistory { get; set; }
        public string PublicLoanOfferHistory { get; set; }
        
        [JsonIgnore]
        public string CurrentLogFile
        {
            get { return string.Format(BaseLogFile, DateTime.Now.Ticks); }
        }

        public BotConfig()
        {
            this.BotFilesFolder = @"C:\lendingbot";
            this.BaseLogFile = @"C:\lendingbot\bitbotlog-{0}.json";
            this.MyLoanHistory = @"C:\lendingbot\my_loan_history.json";
            this.PublicLoanOfferHistory = @"C:\lendingbot\public_loan_offer_history.json";
            
            this.IntervalBetweenLoops = 2 * 60 * 1000;
            this.IntervalBetweenCalls = 15000;
            this.PercentageToLend = 0.5;
        }
    }
}
