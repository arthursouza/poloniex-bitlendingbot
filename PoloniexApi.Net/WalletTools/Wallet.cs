using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Jojatekok.PoloniexAPI.WalletTools
{
    public class Wallet : IWallet
    {
        private ApiWebClient ApiWebClient { get; set; }

        internal Wallet(ApiWebClient apiWebClient)
        {
            ApiWebClient = apiWebClient;
        }

        private IDictionary<string, Balance> GetBalances()
        {
            var postData = new Dictionary<string, object>();

            var data = PostData<IDictionary<string, Balance>>("returnCompleteBalances", postData);
            return data;
        }
        
        private IDictionary<string, LendingBalance> GetAvailableAccountBalances(string account)
        {
            var postData = new Dictionary<string, object> {
                { "account", account },
            };

            var data = PostData<IDictionary<string, LendingBalance>>("returnAvailableAccountBalances", postData);
            return data;
        }

        private List<OpenLoanOffer> GetOpenLoanOffers()
        {
            //TODO
            var postData = new Dictionary<string, object>();
            
            return PostData<List<OpenLoanOffer>>("returnOpenLoanOffers", postData);
        }

        private ActiveLoanList GetActiveLoans()
        {
            var postData = new Dictionary<string, object>();

            var data = PostData<ActiveLoanList>("returnActiveLoans", postData);
            return data;
        }
        
        private IDictionary<string, string> GetDepositAddresses()
        {
            var postData = new Dictionary<string, object>();

            var data = PostData<IDictionary<string, string>>("returnDepositAddresses", postData);
            return data;
        }

        private IDepositWithdrawalList GetDepositsAndWithdrawals(DateTime startTime, DateTime endTime)
        {
            var postData = new Dictionary<string, object> {
                { "start", Helper.DateTimeToUnixTimeStamp(startTime) },
                { "end", Helper.DateTimeToUnixTimeStamp(endTime) }
            };

            var data = PostData<DepositWithdrawalList>("returnDepositsWithdrawals", postData);
            return data;
        }

        private IGeneratedDepositAddress PostGenerateNewDepositAddress(string currency)
        {
            var postData = new Dictionary<string, object> {
                { "currency", currency }
            };

            var data = PostData<IGeneratedDepositAddress>("generateNewAddress", postData);
            return data;
        }

        private void PostWithdrawal(string currency, double amount, string address, string paymentId)
        {
            var postData = new Dictionary<string, object> {
                { "currency", currency },
                { "amount", amount.ToStringNormalized() },
                { "address", address }
            };

            if (paymentId != null) {
                postData.Add("paymentId", paymentId);
            }

            PostData<IGeneratedDepositAddress>("withdraw", postData);
        }
        
        private string CancelOpenLoanOffer(string orderNumber)
        {
            var postData = new Dictionary<string, object> {
                { "orderNumber", orderNumber },
            };
            
            return PostData<string>("cancelLoanOffer", postData);
        }

        private string CreateLoanOffer(CreateLoanOffer model)
        {
            //"currency", "amount", "duration", "autoRenew" (0 or 1), and "lendingRate"
            var postData = new Dictionary<string, object> {
                { "currency", model.Currency },
                { "amount", model.Amount.ToString(new CultureInfo("en-us")) },
                { "lendingRate", model.LendingRate.ToString(new CultureInfo("en-us")) },
                { "duration", 2 },
                { "autoRenew", 0 },
            };
            
            return PostData<string>("createLoanOffer", postData);
        }

        public Task<IDictionary<string, Balance>> GetBalancesAsync()
        {
            return Task.Factory.StartNew(() => GetBalances());
        }

        public Task<IDictionary<string, LendingBalance>> GetAvailableAccountBalancesAsync(string account)
        {
            return Task.Factory.StartNew(() => GetAvailableAccountBalances(account));
        }

        public Task<List<OpenLoanOffer>> GetOpenLoanOffersAsync()
        {
            return Task.Factory.StartNew(() => GetOpenLoanOffers());
        }

        public Task<ActiveLoanList> GetActiveLoansAsync()
        {
            return Task.Factory.StartNew(() => GetActiveLoans());
        }

        public Task<IDictionary<string, string>> GetDepositAddressesAsync()
        {
            return Task.Factory.StartNew(() => GetDepositAddresses());
        }

        public Task<IDepositWithdrawalList> GetDepositsAndWithdrawalsAsync(DateTime startTime, DateTime endTime)
        {
            return Task.Factory.StartNew(() => GetDepositsAndWithdrawals(startTime, endTime));
        }

        public Task<IDepositWithdrawalList> GetDepositsAndWithdrawalsAsync()
        {
            return Task.Factory.StartNew(() => GetDepositsAndWithdrawals(Helper.DateTimeUnixEpochStart, DateTime.MaxValue));
        }

        public Task<IGeneratedDepositAddress> PostGenerateNewDepositAddressAsync(string currency)
        {
            return Task.Factory.StartNew(() => PostGenerateNewDepositAddress(currency));
        }

        public Task PostWithdrawalAsync(string currency, double amount, string address, string paymentId)
        {
            return Task.Factory.StartNew(() => PostWithdrawal(currency, amount, address, paymentId));
        }

        public Task PostWithdrawalAsync(string currency, double amount, string address)
        {
            return Task.Factory.StartNew(() => PostWithdrawal(currency, amount, address, null));
        }
        
        public Task<string> CreateLoanOfferAsync(CreateLoanOffer model)
        {
            return Task<string>.Factory.StartNew(() => CreateLoanOffer(model));
        }

        public Task<string> CancelOpenLoanOfferAsync(string orderNumber)
        {
            return Task<string>.Factory.StartNew(() => CancelOpenLoanOffer(orderNumber));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private T PostData<T>(string command, Dictionary<string, object> postData)
        {
            return ApiWebClient.PostData<T>(command, postData);
        }
    }
}
