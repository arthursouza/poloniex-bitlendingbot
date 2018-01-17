using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Jojatekok.PoloniexAPI.General;

namespace Jojatekok.PoloniexAPI.MarketTools
{
    public class Markets
    {
        internal Markets(ApiWebClient apiWebClient)
        {
            ApiWebClient = apiWebClient;
        }

        private ApiWebClient ApiWebClient { get; }

        private IDictionary<CurrencyPair, MarketData> GetSummary()
        {
            var data = GetData<IDictionary<string, MarketData>>("returnTicker");
            return data.ToDictionary(
                x => CurrencyPair.Parse(x.Key),
                x => x.Value
            );
        }

        private OrderBook GetOpenOrders(CurrencyPair currencyPair, uint depth)
        {
            var data = GetData<OrderBook>(
                "returnOrderBook",
                "currencyPair=" + currencyPair,
                "depth=" + depth
            );
            return data;
        }

        private LoanOrders GetLoanOrders(string currency)
        {
            var data = GetData<LoanOrders>(
                "returnLoanOrders",
                "currency=" + currency
            );
            return data;
        }

        private List<Trade> GetTrades(CurrencyPair currencyPair)
        {
            var data = GetData<List<Trade>>(
                "returnTradeHistory",
                "currencyPair=" + currencyPair
            );
            return new List<Trade>(data);
        }

        private List<Trade> GetTrades(CurrencyPair currencyPair, DateTime startTime, DateTime endTime)
        {
            var data = GetData<List<Trade>>(
                "returnTradeHistory",
                "currencyPair=" + currencyPair,
                "start=" + Helper.DateTimeToUnixTimeStamp(startTime),
                "end=" + Helper.DateTimeToUnixTimeStamp(endTime)
            );
            return new List<Trade>(data);
        }

        private List<MarketChartData> GetChartData(CurrencyPair currencyPair, MarketPeriod period, DateTime startTime, DateTime endTime)
        {
            var data = GetData<List<MarketChartData>>(
                "returnChartData",
                "currencyPair=" + currencyPair,
                "start=" + Helper.DateTimeToUnixTimeStamp(startTime),
                "end=" + Helper.DateTimeToUnixTimeStamp(endTime),
                "period=" + (int) period
            );
            return new List<MarketChartData>(data);
        }

        public Task<IDictionary<CurrencyPair, MarketData>> GetSummaryAsync()
        {
            return Task.Factory.StartNew(() => GetSummary());
        }

        public Task<OrderBook> GetOpenOrdersAsync(CurrencyPair currencyPair, uint depth)
        {
            return Task.Factory.StartNew(() => GetOpenOrders(currencyPair, depth));
        }

        public Task<List<Trade>> GetTradesAsync(CurrencyPair currencyPair)
        {
            return Task.Factory.StartNew(() => GetTrades(currencyPair));
        }

        public Task<LoanOrders> GetLoanOrdersAsync(string currency)
        {
            return Task.Factory.StartNew(() => GetLoanOrders(currency));
        }

        public Task<List<Trade>> GetTradesAsync(CurrencyPair currencyPair, DateTime startTime, DateTime endTime)
        {
            return Task.Factory.StartNew(() => GetTrades(currencyPair, startTime, endTime));
        }

        public Task<List<MarketChartData>> GetChartDataAsync(CurrencyPair currencyPair, MarketPeriod period, DateTime startTime, DateTime endTime)
        {
            return Task.Factory.StartNew(() => GetChartData(currencyPair, period, startTime, endTime));
        }

        public Task<List<MarketChartData>> GetChartDataAsync(CurrencyPair currencyPair, MarketPeriod period)
        {
            return Task.Factory.StartNew(() => GetChartData(currencyPair, period, Helper.DateTimeUnixEpochStart, DateTime.MaxValue));
        }

        public Task<List<MarketChartData>> GetChartDataAsync(CurrencyPair currencyPair)
        {
            return Task.Factory.StartNew(() => GetChartData(currencyPair, MarketPeriod.Minutes30, Helper.DateTimeUnixEpochStart, DateTime.MaxValue));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private T GetData<T>(string command, params object[] parameters)
        {
            return ApiWebClient.GetData<T>(Helper.ApiUrlHttpsRelativePublic + command, parameters);
        }
    }
}