using CurrencyConverter.Models;
using CurrencyConverter.Services;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyConverter.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencyConverterController : ControllerBase
    {
        private readonly ICurrencyConverterService _CurrencyConverterService;
        private readonly ILogger<CurrencyConverterController> _logger;

        public CurrencyConverterController(ICurrencyConverterService CurrencyConverterService, ILogger<CurrencyConverterController> logger)
        {
            _logger = logger;
            _CurrencyConverterService = CurrencyConverterService;
        }

        [HttpGet("convert")]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        [ProducesResponseType(typeof(ExchangeRatesResponse), 200)]
        public async Task<IActionResult> GetCurrency(string sourceCurrency, string targetCurrency, decimal amount)
        {
            var result = await _CurrencyConverterService.GetExchangeRate(sourceCurrency, targetCurrency, amount);

            return Ok(result);
        }
    }
}
