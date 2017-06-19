using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jojatekok.PoloniexAPI.MarketTools;
using Jojatekok.PoloniexAPI.WalletTools;
using Newtonsoft.Json;

namespace Jojatekok.PoloniexAPI
{
    public class Repository
    {
        private string myLoanHistory = @"C:\\lendingbotlogs\my_loan_history.json";
        private string publicLoanOfferHistory = @"C:\\lendingbotlogs\public_loan_offer_history.json";

        public static T LoadObject<T>(string jsonFile) where T : new()
        {
            if (File.Exists(jsonFile))
            {
                var json = new JsonSerializer();
                var jsonString = File.ReadAllText(jsonFile);
                return json.DeserializeObject<T>(jsonString);
            }
            return new T();
        }
        
        public static void SaveObject<T>(T entity, string jsonFile) where T : new()
        {
            var jsonObject = JsonConvert.SerializeObject(entity);
            File.WriteAllText(jsonFile, jsonObject);
        }

        public List<ActiveLoan> GetLoanHistory()
        {
            return LoadObject<List<ActiveLoan>>(myLoanHistory);
        }

        public void SaveLoanHistory(List<ActiveLoan> list)
        {
            SaveObject(list, myLoanHistory);
        }

        public List<LoanOrder> SavePublicLoansAndLoadHistory(LoanOrders loanOrders)
        {
            var loanOffers = LoadObject<List<LoanOrder>>(publicLoanOfferHistory);
            foreach (var order in loanOrders.Offers)
            {
                order.Date = DateTime.Now;
                loanOffers.Add(order);
            }
            if (loanOffers.Count > 5000)
            {
                loanOffers = loanOffers.OrderByDescending(l => l.Date).Take(5000).ToList();
            }
            SaveObject(loanOffers, publicLoanOfferHistory);
            return loanOffers;
        }
    }
}
