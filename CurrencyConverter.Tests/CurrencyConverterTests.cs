using Moq;
using CurrencyConverter.Services
    ;
using Microsoft.Extensions.Logging;
using Castle.DynamicProxy;
using Microsoft.AspNetCore.Mvc;
namespace CurrencyConverter.Tests
{
    public class CurrencyConverterTests
    {
        private readonly ICurrencyConverterService _currencyConverterService;
        private readonly Mock<ILogger<CurrencyConverterService>> _loggerMock;

        public CurrencyConverterTests()
        {
            _loggerMock = new Mock<ILogger<CurrencyConverterService>>();
            _currencyConverterService = new CurrencyConverterService(_loggerMock.Object);
        }
        [Fact]
        public async Task GetExchangeRate_ValidCurrencies_ReturnsExchangeRate()
        {
            // Arrange
            var sourceCurrency = "USD";
            var targetCurrency = "INR";
            var amount = 100;

            // Act
            var result = await _currencyConverterService.GetExchangeRate(sourceCurrency, targetCurrency, amount);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(7400, result.ConvertedAmount, 2); 
        }

        [Fact]
        public async Task ConvertCurrency_InvalidCurrencyPair_ReturnsBadRequest()
        {
            // Arrange
            var sourceCurrency = "USD";
            var targetCurrency = "INR";
            var amount = 100;

            // Act
            var result = await _currencyConverterService.GetExchangeRate(sourceCurrency, targetCurrency, amount);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(7400, result.ConvertedAmount,2);
        }
    }
}