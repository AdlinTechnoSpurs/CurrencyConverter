
using CurrencyConverter.Models;

namespace CurrencyConverter.Services
{
    public interface ICurrencyConverterService
    {
        Task<ExchangeRatesResponse> GetExchangeRate(string sourceCurrency, string targetCurrency, decimal amount);
    }
}
