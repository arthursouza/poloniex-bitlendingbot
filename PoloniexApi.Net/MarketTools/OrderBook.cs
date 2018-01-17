using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace Jojatekok.PoloniexAPI.MarketTools
{
    public class OrderBook
    {
        [JsonProperty("bids")]
        private List<string[]> BuyOrdersInternal
        {
            set { BuyOrders = ParseOrders(value); }
        }

        public List<Order> BuyOrders { get; private set; }

        [JsonProperty("asks")]
        private List<string[]> SellOrdersInternal
        {
            set { SellOrders = ParseOrders(value); }
        }

        public List<Order> SellOrders { get; private set; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static List<Order> ParseOrders(List<string[]> orders)
        {
            var output = new List<Order>(orders.Count);
            for (var i = 0; i < orders.Count; i++)
            {
                output.Add(
                    new Order(
                        double.Parse(orders[i][0], Helper.InvariantCulture),
                        double.Parse(orders[i][1], Helper.InvariantCulture)
                    )
                );
            }
            return output;
        }
    }
}