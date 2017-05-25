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
        
        
        public MainWindow()
        {
            // Set icon from the assembly
            Icon = System.Drawing.Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location).ToImageSource();
            
            InitializeComponent();

            PoloniexClient = new PoloniexClient(ApiKeys.PublicKey, ApiKeys.PrivateKey);
            LoadMarketSummaryAsync();
            
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 1, 0);
            dispatcherTimer.Start();
        }


        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            LoadMarketSummaryAsync();
        }

        private async void LoadMarketSummaryAsync()
        {
            var json = new JsonSerializer();
            var savedLoans = new List<ActiveLoan>();
            var closedLoans = new List<ActiveLoan>();

            if (File.Exists("savedLoans.txt"))
            {
                var jsonString = File.ReadAllText("savedLoans.txt");
                savedLoans = json.DeserializeObject<List<ActiveLoan>>(jsonString);
            }
            if (File.Exists("closedloans.txt"))
            {
                var jsonString = File.ReadAllText("closedloans.txt");
                closedLoans = json.DeserializeObject<List<ActiveLoan>>(jsonString);
            }
            
            var activeLoans = await PoloniexClient.Wallet.GetActiveLoansAsync();

            if (!savedLoans.Any())
            {
                if (activeLoans != null && activeLoans.Provided.Any())
                {
                    savedLoans = activeLoans.Provided.ToList();
                }
            }
            else
            {
                if (activeLoans == null || !activeLoans.Provided.Any())
                {
                    closedLoans = new List<ActiveLoan>(savedLoans);
                    savedLoans.Clear();
                }
                else
                {
                    var recentlyAdded = activeLoans.Provided.Where(al => savedLoans.All(sl => sl.Id != al.Id)).ToList();
                    foreach (var loan in recentlyAdded)
                    {
                        savedLoans.Add(loan);
                    }

                    var recentlyClosed = savedLoans.Where(cl => activeLoans.Provided.All(al => al.Id != cl.Id)).ToList();
                    foreach (var loan in recentlyClosed)
                    {
                        var removed = savedLoans.FirstOrDefault(l => l.Id == loan.Id);
                        savedLoans.Remove(removed);
                    }
                    closedLoans.AddRange(recentlyClosed);
                }
            }

            dataGrid.Name = "Active Loans"

            foreach (var loan in savedLoans)
            {
                var item = new
                {
                    loan.Amount,
                    loan.Date,
                    loan.Rate,
                    loan.Fees,
                    loan.
                }
            }

            

            File.WriteAllText("savedLoans.txt", JsonConvert.SerializeObject(savedLoans));
            File.WriteAllText("closedloans.txt", JsonConvert.SerializeObject(closedLoans));
        }
    }
}
