using System.Diagnostics.CodeAnalysis;

namespace CurrencyConverter.Models
{
    [ExcludeFromCodeCoverage]
    public class ExchangeRates
    {
        public Dictionary<string, decimal> ExchangeValue { get; set; }
    }
}
