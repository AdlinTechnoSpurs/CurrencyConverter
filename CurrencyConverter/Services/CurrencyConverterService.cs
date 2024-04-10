using CurrencyConverter.Models;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace CurrencyConverter.Services
{
    public class CurrencyConverterService : ICurrencyConverterService
    {
        private readonly ILogger<CurrencyConverterService> _logger;

        public CurrencyConverterService(ILogger<CurrencyConverterService> logger)
        {
            _logger = logger;
        }

        public async Task<ExchangeRatesResponse> GetExchangeRate(string sourceCurrency, string targetCurrency, decimal amount)
        {
            var result = new ExchangeRatesResponse();
            bool isvalid = ValidateCurrencyConversion(sourceCurrency, targetCurrency, amount);

            if (isvalid)
            {
                decimal exchangeRate = GetExchangeRates(sourceCurrency, targetCurrency);
                decimal convertedAmount = amount * exchangeRate;

                result = new ExchangeRatesResponse(exchangeRate, convertedAmount);
            }
            return result;
        }

        private decimal GetExchangeRates(string sourceCurrency, string targetCurrency)
        {
            decimal exchangeRate = 0;
            var exchangeRatesFilePath = "exchangeRates.json";

            if (!File.Exists(exchangeRatesFilePath))
            {
                _logger.LogError($"Exchange rates file not found at path: {exchangeRatesFilePath}");
                throw new FileNotFoundException($"Exchange rates file not found at path: {exchangeRatesFilePath}");
            }

            var exchangeRatesJson = File.ReadAllText(exchangeRatesFilePath);

            var exchangeRates = JsonSerializer.Deserialize<Dictionary<string, decimal>>(exchangeRatesJson);

            ApplyOverrides(exchangeRates);

            var exchangeRateKey = $"{sourceCurrency.ToUpper()}_TO_{targetCurrency.ToUpper()}";

            if (exchangeRates.ContainsKey(exchangeRateKey))
            {
                exchangeRate = exchangeRates[exchangeRateKey];
            }
            else
            {
                _logger.LogError("Exchange rate not found for the given currency pair.");
                throw new InvalidOperationException("Exchange rate not found for the given currency pair.");
            }

            return exchangeRate;
        }

        private void ApplyOverrides(Dictionary<string, decimal> exchangeRates)
        {
            var exchangeRatesFilePath = "exchangeRates.json";
            foreach (var key in exchangeRates.Keys)
            {
                var envVarName = key.ToUpper();
                var envVarValue = Environment.GetEnvironmentVariable(envVarName);
                if (!string.IsNullOrWhiteSpace(envVarValue))
                {
                    if (decimal.TryParse(envVarValue, out decimal rate))
                    {
                        exchangeRates[key] = rate;
                        var updatedJsonContent = JsonConvert.SerializeObject(exchangeRates, Formatting.Indented);

                        File.WriteAllText(exchangeRatesFilePath, updatedJsonContent);
                    }
                }
            }
        }

        private bool ValidateCurrencyConversion(string sourceCurrency, string targetCurrency, decimal amount)
        {
            var blnResult = true;
            if (string.IsNullOrWhiteSpace(sourceCurrency) || string.IsNullOrWhiteSpace(targetCurrency))
            {
                blnResult = false;
                _logger.LogError("Invalid input: Currency codes cannot be empty.");
                throw new ArgumentException("Currency codes cannot be empty.");
            }
            if (amount <= 0)
            {
                blnResult = false;
                _logger.LogError("Invalid input: amount is not valid");
                throw new NotSupportedException("amount is not valid.");
            }
            
            return blnResult;
        }

    }
}
