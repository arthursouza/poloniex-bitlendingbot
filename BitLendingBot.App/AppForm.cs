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
        private PoloniexClient poloniexClient;
        private Repository repository;
        private string currentLogFile = @"C:\lendingbotlogs\bitbotlog.json";
        private string btcDollarFile = @"C:\lendingbotlogs\btcdollar.json";

        double btcDollar = 0f;

        private int intervalBetweenLoops = 2 * 60 * 1000;
        private int intervalBetweenCalls = 15000;

        public AppForm()
        {
            InitializeComponent();

            if (!Directory.Exists(@"C:\lendingbotlogs"))
            {
                Directory.CreateDirectory(@"C:\lendingbotlogs");
            }

            poloniexClient = new PoloniexClient(ApiKeys.PublicKey, ApiKeys.PrivateKey);
            repository = new Repository();

            // generate a log file for this execution
            currentLogFile += " " + DateTime.Now.Ticks + ".txt";

            btcDollar = Repository.LoadObject<double>(btcDollarFile);
            txtBTCDollar.Text = btcDollar.ToString("0.00000000");

            // initial call to the information gathering method
            ProcessInformation();

            timer.Interval = 60 * 1000;
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
            var avgRate30minBottom10 = loanOfferHistory.Where(l => l.Date >= DateTime.Now.AddMinutes(-30)).OrderBy(l => l.Date).Take(10).Average(l => l.Rate);
            return avgRate30minBottom10;
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
                var loanHistory = repository.GetLoanHistory();
                ProcessLoanHistoryData(loanHistory);
                
                if (botLog.Text.Length > 10000)
                {
                    botLog.Text = string.Empty;
                }

                var loans = await poloniexClient.Markets.GetLoanOrdersAsync("BTC");
                var loanOfferHistory = repository.SavePublicLoansAndLoadHistory(loans);
                
                // perhaps I should only check the rates of current offers to be more competitive
                var highrate = CalculateHighLoanRate(loanOfferHistory);
                var lowrate = CalculateLowLoanRate(loanOfferHistory);

                LogLine($"Checking current rates: Low {lowrate:P4} High {highrate:P4}");

                // wait to avoid being blocked by poloniex
                Thread.Sleep(intervalBetweenCalls);
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

                repository.SaveLoanHistory(loanHistory);

                // wait to avoid being blocked by poloniex
                Thread.Sleep(intervalBetweenCalls);
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
                            Thread.Sleep(intervalBetweenCalls);
                            var result = await poloniexClient.Wallet.CancelOpenLoanOfferAsync(loan.Id.ToString());
                            LogLine("Return from cancelLoanOffer command: " + result);
                        }
                    }
                }


                // wait to avoid being blocked by poloniex
                Thread.Sleep(500);
                // get current lending balance
                var balances = await poloniexClient.Wallet.GetAvailableAccountBalancesAsync("lending");
                // reopen loan offers based on the updated rates
                
                if (balances.ContainsKey("lending"))
                {
                    var lendingBalance = balances["lending"];
                    
                    if (lendingBalance.BTC.HasValue)
                    {
                        var offerCreated = new CreateLoanOffer()
                        {
                            Amount = lendingBalance.BTC.Value,
                            LendingRate = lowrate,
                            Currency = "BTC"
                        };

                        LogLine($"Available lending balance: {offerCreated.Amount:0.00000000} BTC");

                        if (lendingBalance.BTC.Value > 0.01)
                        {
                            // wait to avoid being blocked by poloniex
                            Thread.Sleep(intervalBetweenCalls);
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

        private void ProcessLoanHistoryData(List<ActiveLoan> loanHistory)
        {
            var lastYear = loanHistory.Where(l => l.Date >= DateTime.Now.AddYears(-1)).SumEarned();
            var lastMonth = loanHistory.Where(l => l.Date >= DateTime.Now.AddMonths(-1)).SumEarned();
            var lastWeek = loanHistory.Where(l => l.Date >= DateTime.Now.AddDays(-7)).SumEarned();
            var lastDay = loanHistory.Where(l => l.Date >= DateTime.Now.AddDays(-1)).SumEarned();
            var lastHour = loanHistory.Where(l => l.Date >= DateTime.Now.AddHours(-1)).SumEarned();
            var total = loanHistory.SumEarned();

            txtDayBTC.Text = lastDay.ToString("0.00000000");
            txtHourBtc.Text = lastHour.ToString("0.00000000");
            txtWeekBTC.Text = lastWeek.ToString("0.00000000");
            txtMonthBTC.Text = lastMonth.ToString("0.00000000");
            txtYearBtc.Text = lastYear.ToString("0.00000000");
            txtTotalBTC.Text = total.ToString("0.00000000");

            txtDollarHour.Text = (lastHour * btcDollar).ToString("0.00000000");
            txtDollarDay.Text = (lastDay * btcDollar).ToString("0.00000000");
            txtDollarWeek.Text = (lastWeek * btcDollar).ToString("0.00000000");
            txtDollarMonth.Text = (lastMonth * btcDollar).ToString("0.00000000");
            txtDollarYear.Text = (lastYear * btcDollar).ToString("0.00000000");
            txtDollarTotal.Text = (total * btcDollar).ToString("0.00000000");
        }

        private void LogLine(string text)
        {
            botLog.AppendText(DateTime.Now.ToString("dd/MM HH:mm:ss") + ": " + text + Environment.NewLine);
            //botLog.Text += DateTime.Now.ToString("dd/MM HH:mm:ss") + ": " + text + Environment.NewLine;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            // start the background worker
            worker.RunWorkerAsync();
        }

        private void txtBTCDollar_TextChanged(object sender, EventArgs e)
        {
            double.TryParse(txtBTCDollar.Text, out btcDollar);
            Repository.SaveObject(btcDollar, currentLogFile);
        }
    }

    public static class MyExtensions
    {
        public static double SumEarned(this IEnumerable<ActiveLoan> list)
        {
            return list.Sum(l => DateTime.Now.ToUniversalTime().Subtract(l.Date).TotalDays * l.Rate * l.Amount);
        }
    }
}
