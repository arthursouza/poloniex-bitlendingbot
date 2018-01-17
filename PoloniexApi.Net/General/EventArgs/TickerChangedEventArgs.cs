using Jojatekok.PoloniexAPI.MarketTools;

namespace Jojatekok.PoloniexAPI.General.EventArgs
{
    public class TickerChangedEventArgs : System.EventArgs
    {
        internal TickerChangedEventArgs(CurrencyPair currencyPair, MarketData marketData)
        {
            CurrencyPair = currencyPair;
            MarketData = marketData;
        }

        public CurrencyPair CurrencyPair { get; }
        public MarketData MarketData { get; }
    }
}