using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Jojatekok.PoloniexAPI.General;
using Newtonsoft.Json;

namespace Jojatekok.PoloniexAPI.WalletTools
{
    public class Wallet
    {
        internal Wallet(ApiWebClient apiWebClient)
        {
            ApiWebClient = apiWebClient;
        }

        private ApiWebClient ApiWebClient { get; }

        private Dictionary<string, Balance> GetBalances()
        {
            var postData = new Dictionary<string, object>();

            var data = PostData<Dictionary<string, Balance>>("returnCompleteBalances", postData);
            return data;
        }

        private Dictionary<string, LendingBalance> GetAvailableAccountBalances(string account)
        {
            var postData = new Dictionary<string, object>
            {
                {"account", account}
            };

            var data = PostData<Dictionary<string, object>>("returnAvailableAccountBalances", postData);

            if (!string.IsNullOrEmpty(account))
            {
                var balance = data[account];
                var js = new JsonSerializer {NullValueHandling = NullValueHandling.Ignore};
                try
                {
                    var balanceValue = js.DeserializeObject<LendingBalance>(balance.ToString());
                    if (balanceValue != null)
                    {
                        return new Dictionary<string, LendingBalance>
                        {
                            {"lending", balanceValue}
                        };
                    }
                }
                catch
                {
                    // ignore
                }
            }

            return new Dictionary<string, LendingBalance>();
        }

        private Dictionary<string, List<OpenLoanOffer>> GetOpenLoanOffers()
        {
            //TODO
            var postData = new Dictionary<string, object>();

            return PostData<Dictionary<string, List<OpenLoanOffer>>>("returnOpenLoanOffers", postData);
        }

        private ActiveLoanList GetActiveLoans()
        {
            var postData = new Dictionary<string, object>();

            var data = PostData<ActiveLoanList>("returnActiveLoans", postData);
            return data;
        }

        private Dictionary<string, string> GetDepositAddresses()
        {
            var postData = new Dictionary<string, object>();

            var data = PostData<Dictionary<string, string>>("returnDepositAddresses", postData);
            return data;
        }

        private DepositWithdrawalList GetDepositsAndWithdrawals(DateTime startTime, DateTime endTime)
        {
            var postData = new Dictionary<string, object>
            {
                {"start", Helper.DateTimeToUnixTimeStamp(startTime)},
                {"end", Helper.DateTimeToUnixTimeStamp(endTime)}
            };

            var data = PostData<DepositWithdrawalList>("returnDepositsWithdrawals", postData);
            return data;
        }

        private GeneratedDepositAddress PostGenerateNewDepositAddress(string currency)
        {
            var postData = new Dictionary<string, object>
            {
                {"currency", currency}
            };

            var data = PostData<GeneratedDepositAddress>("generateNewAddress", postData);
            return data;
        }

        private void PostWithdrawal(string currency, double amount, string address, string paymentId)
        {
            var postData = new Dictionary<string, object>
            {
                {"currency", currency},
                {"amount", amount.ToStringNormalized()},
                {"address", address}
            };

            if (paymentId != null)
            {
                postData.Add("paymentId", paymentId);
            }

            PostData<GeneratedDepositAddress>("withdraw", postData);
        }

        private object CancelOpenLoanOffer(string orderNumber)
        {
            var postData = new Dictionary<string, object>
            {
                {"orderNumber", orderNumber}
            };

            return PostData<object>("cancelLoanOffer", postData);
        }

        private object CreateLoanOffer(CreateLoanOffer model)
        {
            //"currency", "amount", "duration", "autoRenew" (0 or 1), and "lendingRate"
            var postData = new Dictionary<string, object>
            {
                {"currency", model.Currency},
                {"amount", model.Amount.ToString(new CultureInfo("en-us"))},
                {"lendingRate", model.LendingRate.ToString(new CultureInfo("en-us"))},
                {"duration", 2},
                {"autoRenew", 0}
            };

            return PostData<object>("createLoanOffer", postData);
        }

        public Task<Dictionary<string, Balance>> GetBalancesAsync()
        {
            return Task.Factory.StartNew(() => GetBalances());
        }

        public Task<Dictionary<string, LendingBalance>> GetAvailableAccountBalancesAsync(string account)
        {
            return Task.Factory.StartNew(() => GetAvailableAccountBalances(account));
        }

        public Task<Dictionary<string, List<OpenLoanOffer>>> GetOpenLoanOffersAsync()
        {
            return Task.Factory.StartNew(() => GetOpenLoanOffers());
        }

        public Task<ActiveLoanList> GetActiveLoansAsync()
        {
            return Task.Factory.StartNew(() => GetActiveLoans());
        }

        public Task<Dictionary<string, string>> GetDepositAddressesAsync()
        {
            return Task.Factory.StartNew(() => GetDepositAddresses());
        }

        public Task<DepositWithdrawalList> GetDepositsAndWithdrawalsAsync(DateTime startTime, DateTime endTime)
        {
            return Task.Factory.StartNew(() => GetDepositsAndWithdrawals(startTime, endTime));
        }

        public Task<DepositWithdrawalList> GetDepositsAndWithdrawalsAsync()
        {
            return Task.Factory.StartNew(() => GetDepositsAndWithdrawals(Helper.DateTimeUnixEpochStart, DateTime.MaxValue));
        }

        public Task<GeneratedDepositAddress> PostGenerateNewDepositAddressAsync(string currency)
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

        public Task<object> CreateLoanOfferAsync(CreateLoanOffer model)
        {
            return Task<object>.Factory.StartNew(() => CreateLoanOffer(model));
        }

        public Task<object> CancelOpenLoanOfferAsync(string orderNumber)
        {
            return Task<object>.Factory.StartNew(() => CancelOpenLoanOffer(orderNumber));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private T PostData<T>(string command, Dictionary<string, object> postData) where T : new()
        {
            return ApiWebClient.PostData<T>(command, postData);
        }
    }
}