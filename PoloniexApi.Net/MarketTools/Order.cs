namespace Jojatekok.PoloniexAPI.MarketTools
{
    public class Order
    {
        internal Order(double pricePerCoin, double amountQuote)
        {
            PricePerCoin = pricePerCoin;
            AmountQuote = amountQuote;
        }

        internal Order()
        {
        }

        public double PricePerCoin { get; }

        public double AmountQuote { get; }

        public double AmountBase => (AmountQuote * PricePerCoin).Normalize();
    }
}