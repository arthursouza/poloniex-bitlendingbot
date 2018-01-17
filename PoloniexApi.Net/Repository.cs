using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Jojatekok.PoloniexAPI.MarketTools;
using Jojatekok.PoloniexAPI.WalletTools;
using Newtonsoft.Json;

namespace Jojatekok.PoloniexAPI
{
    public class Repository
    {
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

        public List<ActiveLoan> GetLoanHistory(string filepath)
        {
            return LoadObject<List<ActiveLoan>>(filepath);
        }

        public void SaveLoanHistory(List<ActiveLoan> list, string filepath)
        {
            SaveObject(list, filepath);
        }

        public List<LoanOrder> SavePublicLoansAndLoadHistory(LoanOrders loanOrders, string filepath)
        {
            var loanOffers = LoadObject<List<LoanOrder>>(filepath);
            foreach (var order in loanOrders.Offers)
            {
                order.Date = DateTime.Now;
                loanOffers.Add(order);
            }
            if (loanOffers.Count > 5000)
            {
                loanOffers = loanOffers.OrderByDescending(l => l.Date).Take(5000).ToList();
            }
            SaveObject(loanOffers, filepath);
            return loanOffers;
        }
    }
}