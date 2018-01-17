﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Jojatekok.PoloniexAPI.General;
using Newtonsoft.Json.Linq;

namespace Jojatekok.PoloniexAPI.TradingTools
{
    public class Trading
    {
        internal Trading(ApiWebClient apiWebClient)
        {
            ApiWebClient = apiWebClient;
        }

        private ApiWebClient ApiWebClient { get; }

        private List<Order> GetOpenOrders(CurrencyPair currencyPair)
        {
            var postData = new Dictionary<string, object>
            {
                {"currencyPair", currencyPair}
            };

            var data = PostData<List<Order>>("returnOpenOrders", postData);
            return data;
        }

        private List<Trade> GetTrades(CurrencyPair currencyPair, DateTime startTime, DateTime endTime)
        {
            var postData = new Dictionary<string, object>
            {
                {"currencyPair", currencyPair},
                {"start", Helper.DateTimeToUnixTimeStamp(startTime)},
                {"end", Helper.DateTimeToUnixTimeStamp(endTime)}
            };

            var data = PostData<List<Trade>>("returnTradeHistory", postData);
            return data;
        }

        private ulong PostOrder(CurrencyPair currencyPair, OrderType type, double pricePerCoin, double amountQuote)
        {
            var postData = new Dictionary<string, object>
            {
                {"currencyPair", currencyPair},
                {"rate", pricePerCoin.ToStringNormalized()},
                {"amount", amountQuote.ToStringNormalized()}
            };

            var data = PostData<JObject>(type.ToStringNormalized(), postData);
            return data.Value<ulong>("orderNumber");
        }

        private bool DeleteOrder(CurrencyPair currencyPair, ulong orderId)
        {
            var postData = new Dictionary<string, object>
            {
                {"currencyPair", currencyPair},
                {"orderNumber", orderId}
            };

            var data = PostData<JObject>("cancelOrder", postData);
            return data.Value<byte>("success") == 1;
        }

        public Task<List<Order>> GetOpenOrdersAsync(CurrencyPair currencyPair)
        {
            return Task.Factory.StartNew(() => GetOpenOrders(currencyPair));
        }

        public Task<List<Trade>> GetTradesAsync(CurrencyPair currencyPair, DateTime startTime, DateTime endTime)
        {
            return Task.Factory.StartNew(() => GetTrades(currencyPair, startTime, endTime));
        }

        public Task<List<Trade>> GetTradesAsync(CurrencyPair currencyPair)
        {
            return Task.Factory.StartNew(() => GetTrades(currencyPair, Helper.DateTimeUnixEpochStart, DateTime.MaxValue));
        }

        public Task<ulong> PostOrderAsync(CurrencyPair currencyPair, OrderType type, double pricePerCoin, double amountQuote)
        {
            return Task.Factory.StartNew(() => PostOrder(currencyPair, type, pricePerCoin, amountQuote));
        }

        public Task<bool> DeleteOrderAsync(CurrencyPair currencyPair, ulong orderId)
        {
            return Task.Factory.StartNew(() => DeleteOrder(currencyPair, orderId));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private T PostData<T>(string command, Dictionary<string, object> postData) where T : new()
        {
            return ApiWebClient.PostData<T>(command, postData);
        }
    }
}