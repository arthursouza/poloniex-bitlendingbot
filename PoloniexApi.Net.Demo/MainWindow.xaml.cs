using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Jojatekok.PoloniexAPI.MarketTools;
using Jojatekok.PoloniexAPI.WalletTools;
using Newtonsoft.Json;

namespace Jojatekok.PoloniexAPI.Demo
{
    public sealed partial class MainWindow
    {
        private PoloniexClient PoloniexClient { get; set; }
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

        private Repository repository;
        private string currentLogFile = "bitbotlog";
        public MainWindow()
        {
            // Set icon from the assembly
            Icon = System.Drawing.Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location).ToImageSource();
            
            InitializeComponent();

            PoloniexClient = new PoloniexClient(ApiKeys.PublicKey, ApiKeys.PrivateKey);
            LoadInformation();
            
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 1, 0);
            dispatcherTimer.Start();

            repository = new Repository();

            // generate a log file for this execution
            currentLogFile += DateTime.Now.Ticks.ToString() +".txt";
        }


        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            LoadInformation();
        }

        private void LogLine(string text)
        {
            LendingLog.Text += DateTime.Now.ToString("dd/MM HH:mm:ss")+ ": " + text + Environment.NewLine;
        }

        private async void LoadInformation()
        {
            try
            {
                var loans = await PoloniexClient.Markets.GetLoanOrdersAsync("BTC");
                var loanOfferHistory = repository.SaveCurrentLoanOrders(loans);

                // perhaps I should only check the rates of current offers to be more competitive
                var highrate = CalculateHighLoanRate(loanOfferHistory);
                var lowrate = CalculateLowLoanRate(loanOfferHistory);

                LogLine(string.Format("Checking current rates: Low {0} High {1}", lowrate.ToString("P"), highrate.ToString("P")));

                // get your active loans
                var activeLoans = await PoloniexClient.Wallet.GetActiveLoansAsync();
                var openLoanOffers = await PoloniexClient.Wallet.GetOpenLoanOffersAsync();

                if (activeLoans.Provided.Any())
                {
                    LogLine(string.Format("Currently {0} active loans.", activeLoans.Provided.Count));
                    double totalEarned = 0;
                    double totalAmount = 0;
                    double avgRate = 0;

                    foreach (var loan in activeLoans.Provided)
                    {
                        var earned = (DateTime.Now.Subtract(loan.Date).TotalDays * loan.Rate * loan.Amount);
                        totalEarned += earned;
                        avgRate += loan.Rate;
                        totalAmount += loan.Amount;

                        LogLine(string.Format("{0} BTC || {1} rate || {2} earned", loan.Amount, loan.Rate.ToString("P"), earned));
                    }

                    LogLine(string.Format("Total Active || {0} BTC || {1} rate || {2} earned", totalAmount, (avgRate / activeLoans.Provided.Count).ToString("P"), totalEarned));
                }
                else
                {
                    LogLine("No active loans.");
                }

                //createLoanOffer
                //Creates a loan offer for a given currency.Required POST parameters are "currency", "amount", "duration", "autoRenew"(0 or 1), and "lendingRate".Sample output:
                //{ "success":1,"message":"Loan order placed.","orderID":10590}

                //cancelLoanOffer
                //Cancels a loan offer specified by the "orderNumber" POST parameter. Sample output:
                //{ "success":1,"message":"Loan offer canceled."}

                if (openLoanOffers.Any())
                {
                    LogLine(string.Format("Reopening {0} loan offers.", openLoanOffers.Count));
                }

                // cancel the loan offers currently open
                foreach (var loan in openLoanOffers)
                {
                    var result = await PoloniexClient.Wallet.CancelOpenLoanOfferAsync(loan.Id.ToString());
                }

                // get current lending balance
                var balances = await PoloniexClient.Wallet.GetAvailableAccountBalancesAsync("lending");
                var btcBalance = balances["lending"];

                // reopen loan offers based on the updated rates
                if (btcBalance.BTC > 0)
                {
                    var offerCreated = new CreateLoanOffer()
                    {
                        Amount = btcBalance.BTC.Value,
                        LendingRate = lowrate,
                        Currency = "BTC"
                    };

                    var createLoanOfferResult = await PoloniexClient.Wallet.CreateLoanOfferAsync(offerCreated);
                    LogLine(string.Format("Loan offer created: {0} BTC - {1} rate", offerCreated.Amount, offerCreated.LendingRate.ToString("P")));
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
                File.WriteAllText(currentLogFile, LendingLog.Text);
            }
        }

        private double CalculateLowLoanRate(List<LoanOrder> loanOfferHistory)
        {
            var avgRate30minBottom10 = loanOfferHistory.Where(l => l.Date >= DateTime.Now.AddMinutes(-30)).OrderBy(l => l.Date).Take(10).Average(l => l.Rate);
            return avgRate30minBottom10;
        }

        private double CalculateHighLoanRate(List<LoanOrder> loanOfferHistory)
        {
            var avgRate30minBottom40 = loanOfferHistory.Where(l => l.Date >= DateTime.Now.AddMinutes(-30)).OrderBy(l => l.Date).Take(40).Average(l => l.Rate);
            return avgRate30minBottom40;
        }
    }
}
