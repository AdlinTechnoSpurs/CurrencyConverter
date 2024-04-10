using System.Diagnostics.CodeAnalysis;

namespace CurrencyConverter.Models
{
    [ExcludeFromCodeCoverage]
    public class ExchangeRatesResponse
    {
        public decimal ExchangeRate { get; set; }

        public decimal ConvertedAmount { get; set; }

        public ExchangeRatesResponse()
        {
        }
        public ExchangeRatesResponse(decimal exchangeRate, decimal convertedAmount)
        {
            ExchangeRate = exchangeRate;
            ConvertedAmount = convertedAmount;
        }
    }
}
