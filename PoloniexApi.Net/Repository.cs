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
        private string loanDemandHistory = "loan_demand_history.json";
        private string loanOfferHistory = "loan_offer_history.json";

        private T LoadObject<T>(string jsonFile) where T : new()
        {
            if (File.Exists(jsonFile))
            {
                var json = new JsonSerializer();
                var jsonString = File.ReadAllText(jsonFile);
                return json.DeserializeObject<T>(jsonString);
            }
            return new T();
        }
        
        private void SaveObject<T>(T entity, string jsonFile) where T : new()
        {
            var jsonObject = JsonConvert.SerializeObject(entity);
            File.WriteAllText(jsonFile, jsonObject);
        }

        public List<LoanOrder> SaveCurrentLoanOrders(LoanOrders loanOrders)
        {
            var loanOffers = LoadObject<List<LoanOrder>>(loanOfferHistory);
            //var loanDemands = LoadObject<List<LoanOrder>>(loanDemandHistory);

            //foreach (var order in loanOrders.Demands)
            //{
            //    order.Date = DateTime.Now;
            //    loanDemands.Add(order);
            //}

            foreach (var order in loanOrders.Offers)
            {
                order.Date = DateTime.Now;
                loanOffers.Add(order);
            }

            //if (loanDemands.Count > 5000)
            //{
            //    loanDemands = loanDemands.OrderByDescending(l => l.Date).Take(5000).ToList();
            //}
            if (loanOffers.Count > 5000)
            {
                loanOffers = loanOffers.OrderByDescending(l => l.Date).Take(5000).ToList();
            }

            //SaveObject(loanDemands, loanDemandHistory);
            SaveObject(loanOffers, loanOfferHistory);

            return loanOffers;
        }

        public void SaveCurrentLendingbalance(double balance)
        {
            SaveObject(balance, "lendingbalance.json");
        }
    }
}
