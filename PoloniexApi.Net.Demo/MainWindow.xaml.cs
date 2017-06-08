using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Jojatekok.PoloniexAPI.WalletTools;
using Newtonsoft.Json;

namespace Jojatekok.PoloniexAPI.Demo
{
    public sealed partial class MainWindow
    {
        private PoloniexClient PoloniexClient { get; set; }
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

        private List<ActiveLoan> myLoanHistory;
        private List<ActiveLoan> publicLoanHistory;

        private List<ActiveLoan> myLoans;

        
        public MainWindow()
        {
            // Set icon from the assembly
            Icon = System.Drawing.Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location).ToImageSource();
            
            InitializeComponent();

            PoloniexClient = new PoloniexClient(ApiKeys.PublicKey, ApiKeys.PrivateKey);
            LoadInformation();
            
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 20);
            dispatcherTimer.Start();
        }


        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            LoadInformation();
        }

        private async void LoadInformation()
        {
            //var activeLoans = await PoloniexClient.Wallet.GetActiveLoansAsync();

            var loans = await PoloniexClient.Markets.GetLoanOrdersAsync("BTC");



            //foreach (var loan in activeLoans.Provided)
            //{
            //    var profit = loan.Amount * DateTime.Now.Subtract(loan.Date).TotalDays * loan.Rate;

            //    var item = new
            //    {
            //        loan.Amount,
            //        loan.Date,
            //        loan.Rate,
            //        loan.Fees,
            //        Profit = profit,
            //    };

            //    //if(dataGrid.Items.)


            //    dataGrid.Items.Add(item);
            //}
        }

        private void dataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}
