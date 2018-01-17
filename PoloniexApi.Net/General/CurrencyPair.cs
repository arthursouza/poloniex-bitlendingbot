namespace Jojatekok.PoloniexAPI.General
{
    public class CurrencyPair
    {
        private const char SeparatorCharacter = '_';

        public CurrencyPair(string baseCurrency, string quoteCurrency)
        {
            BaseCurrency = baseCurrency;
            QuoteCurrency = quoteCurrency;
        }

        public string BaseCurrency { get; }
        public string QuoteCurrency { get; }

        public static CurrencyPair Parse(string currencyPair)
        {
            var valueSplit = currencyPair.Split(SeparatorCharacter);
            return new CurrencyPair(valueSplit[0], valueSplit[1]);
        }

        public override string ToString()
        {
            return BaseCurrency + SeparatorCharacter + QuoteCurrency;
        }

        public static bool operator ==(CurrencyPair a, CurrencyPair b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }
            if (((object) a == null) ^ ((object) b == null))
            {
                return false;
            }

            return a.BaseCurrency == b.BaseCurrency && a.QuoteCurrency == b.QuoteCurrency;
        }

        public static bool operator !=(CurrencyPair a, CurrencyPair b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            var b = obj as CurrencyPair;
            return (object) b != null && Equals(b);
        }

        public bool Equals(CurrencyPair b)
        {
            return b.BaseCurrency == BaseCurrency && b.QuoteCurrency == QuoteCurrency;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}