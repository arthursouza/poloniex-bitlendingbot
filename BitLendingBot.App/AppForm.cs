using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Jojatekok.PoloniexAPI;
using Jojatekok.PoloniexAPI.MarketTools;
using Jojatekok.PoloniexAPI.WalletTools;

namespace BitLendingBot.App
{
    public partial class AppForm : Form
    {
        private readonly PoloniexClient poloniexClient;
        private readonly Repository repository;
        private readonly BotConfig config;
        
        public AppForm()
        {
            InitializeComponent();

            if (!File.Exists("config.json"))
            {
                config = new BotConfig();
                Repository.SaveObject(config, "config.json");
            }
            else
            {
                config = Repository.LoadObject<BotConfig>("config.json");
            }
            
            if (!Directory.Exists(config.BotFilesFolder))
            {
                Directory.CreateDirectory(config.BotFilesFolder);
            }
            
            poloniexClient = new PoloniexClient(ApiKeys.PublicKey, ApiKeys.PrivateKey);
            repository = new Repository();

            // generate a log file for this execution
            // initial call to the information gathering method
            ProcessInformation();

            timer.Interval = config.IntervalBetweenLoops;
            timer.Enabled = true;
            timer.Start();

            worker.DoWork += Worker_DoWork;
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            ProcessInformation();
        }

        private static double CalculateLowLoanRate(IEnumerable<LoanOrder> loanOfferHistory)
        {
            var avgRate30MinBottom10 = loanOfferHistory.Where(l => l.Date >= DateTime.Now.AddMinutes(-30)).OrderBy(l => l.Date).Take(10).Average(l => l.Rate);
            return avgRate30MinBottom10;
        }

        private static double CalculateHighLoanRate(IEnumerable<LoanOrder> loanOfferHistory)
        {
            var avgRate30minBottom40 = loanOfferHistory.Where(l => l.Date >= DateTime.Now.AddMinutes(-30)).OrderBy(l => l.Date).Take(40).Average(l => l.Rate);
            return avgRate30minBottom40;
        }

        private async void ProcessInformation()
        {
            try
            {
                var loanHistory = repository.GetLoanHistory(config.MyLoanHistory);
                
                if (botLog.Text.Length > 10000)
                {
                    botLog.Text = string.Empty;
                }

                var loans = await poloniexClient.Markets.GetLoanOrdersAsync("BTC");
                var loanOfferHistory = repository.SavePublicLoansAndLoadHistory(loans, config.PublicLoanOfferHistory);
                
                // perhaps I should only check the rates of current offers to be more competitive
                var highrate = CalculateHighLoanRate(loanOfferHistory);
                var lowrate = CalculateLowLoanRate(loanOfferHistory);

                LogLine($"Checking current rates: Low {lowrate:P4} High {highrate:P4}");

                // wait to avoid being blocked by poloniex
                Thread.Sleep(config.IntervalBetweenCalls);
                // get your active loans
                var activeLoans = await poloniexClient.Wallet.GetActiveLoansAsync();

                #region Log current active loans
                if (activeLoans.Provided.Any())
                {
                    LogLine($"Currently {activeLoans.Provided.Count} active loans.");
                    double totalEarned = 0;
                    double totalAmount = 0;
                    double avgRate = 0;

                    foreach (var loan in activeLoans.Provided)
                    {
                        var earned = (DateTime.Now.ToUniversalTime().Subtract(loan.Date).TotalDays * loan.Rate * loan.Amount);
                        totalEarned += earned;
                        avgRate += loan.Rate;
                        totalAmount += loan.Amount;

                        LogLine($"{loan.Amount:0.00000000} BTC || {loan.Rate:P4} rate || {earned:0.00000000} earned");

                        if (!loanHistory.Any(ml => ml.Id == loan.Id && ml.Date.Date == loan.Date.Date))
                        {
                            loanHistory.Add(loan);
                        }
                    }

                    LogLine($"Total Active || {totalAmount:0.00000000} BTC || {(avgRate / activeLoans.Provided.Count):P4} rate || {totalEarned:0.00000000} earned");
                }
                else
                {
                    LogLine("No active loans.");
                }
                #endregion

                repository.SaveLoanHistory(loanHistory, config.MyLoanHistory);

                // wait to avoid being blocked by poloniex
                Thread.Sleep(config.IntervalBetweenCalls);
                var openLoanOffers = await poloniexClient.Wallet.GetOpenLoanOffersAsync();
                if (openLoanOffers.Any() && openLoanOffers.ContainsKey("BTC"))
                {
                    if (openLoanOffers["BTC"].Any())
                    {
                        LogLine($"Reopening {openLoanOffers["BTC"].Count} loan offers.");
                        // cancel the loan offers currently open
                        foreach (var loan in openLoanOffers["BTC"])
                        {
                            // wait to avoid being blocked by poloniex
                            Thread.Sleep(config.IntervalBetweenCalls);
                            var result = await poloniexClient.Wallet.CancelOpenLoanOfferAsync(loan.Id.ToString());
                            LogLine("Return from cancelLoanOffer command: " + result);
                        }
                    }
                }
                
                // wait to avoid being blocked by poloniex
                Thread.Sleep(config.IntervalBetweenCalls);
                // get current lending balance
                var balances = await poloniexClient.Wallet.GetAvailableAccountBalancesAsync("lending");
                // reopen loan offers based on the updated rates

                // wait to avoid being blocked by poloniex
                Thread.Sleep(config.IntervalBetweenCalls);

                if (balances.ContainsKey("lending"))
                {
                    var lendingBalance = balances["lending"];
                    
                    if (lendingBalance.BTC.HasValue)
                    {
                        var lendingBalanceBTC = lendingBalance.BTC.Value;
                        
                        LogLine($"Available lending balance: {lendingBalanceBTC:0.00000000} BTC");

                        var amountCurrentlyLent = activeLoans.Provided.Any() ? activeLoans.Provided.Sum(al => al.Amount) : 0;
                        var totalBalance = lendingBalanceBTC + amountCurrentlyLent;
                        var amountLeftToLend = totalBalance * config.PercentageToLend - amountCurrentlyLent;

                        if (amountLeftToLend > 0.01)
                        {
                            var offerCreated = new CreateLoanOffer()
                            {
                                Amount = amountLeftToLend,
                                LendingRate = lowrate,
                                Currency = "BTC"
                            };

                            var createLoanOfferResult = await poloniexClient.Wallet.CreateLoanOfferAsync(offerCreated);
                            LogLine($"Loan offer created: {offerCreated.Amount:0.00000000} BTC - {offerCreated.LendingRate:P4} rate");
                        }
                        else
                        {
                            LogLine("Skipping low available lending balance");
                        }
                    }
                }

                // get total value in open offers
                // get total value in open lends
                // get total value in lending balance
                // get total value in lending earnings
                // list earnings in usd and btc
            }
            catch (Exception ex)
            {
                LogLine("Error: " + ex.ToString());
            }
            finally
            {
                //File.WriteAllText(currentLogFile, botLog.Text);
            }
        }
        
        private void LogLine(string text)
        {
            botLog.AppendText(DateTime.Now.ToString("dd/MM HH:mm:ss") + ": " + text + Environment.NewLine);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            // start the background worker
            worker.RunWorkerAsync();
        }
    }
}
