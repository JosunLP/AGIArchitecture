using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AGI.APIIntegration;
using Newtonsoft.Json.Linq;

namespace AGI.WebInterface.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExternalDataController(ExtendedAPIIntegrationService apiIntegrationService) : ControllerBase
    {
        private readonly ExtendedAPIIntegrationService _apiIntegrationService = apiIntegrationService;

        // Endpoint to fetch data from an external API
        [HttpGet("fetch-external-data")]
        public async Task<IActionResult> FetchExternalData([FromQuery] string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return BadRequest("URL parameter is required.");
            }

            JObject data;
            try
            {
                data = await _apiIntegrationService.FetchExternalDataAsync(url);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"Error fetching data: {ex.Message}");
            }

            return Ok(data);
        }

        // Endpoint to fetch financial data (specific example)
        [HttpGet("fetch-financial-data")]
        public async Task<IActionResult> FetchFinancialData([FromQuery] string stockSymbol)
        {
            if (string.IsNullOrEmpty(stockSymbol))
            {
                return BadRequest("Stock symbol is required.");
            }

            JObject financialData;
            try
            {
                financialData = await _apiIntegrationService.FetchFinancialDataAsync(stockSymbol);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"Error fetching financial data: {ex.Message}");
            }

            return Ok(financialData);
        }

        // Endpoint to fetch weather data (specific example)
        [HttpGet("fetch-weather-data")]
        public async Task<IActionResult> FetchWeatherData([FromQuery] string city)
        {
            if (string.IsNullOrEmpty(city))
            {
                return BadRequest("City name is required.");
            }

            JObject weatherData;
            try
            {
                weatherData = await _apiIntegrationService.FetchWeatherDataAsync(city);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"Error fetching weather data: {ex.Message}");
            }

            return Ok(weatherData);
        }

        // Endpoint to trigger automated data fetch (e.g., financial and weather data)
        [HttpPost("automated-fetch")]
        public async Task<IActionResult> AutomatedDataFetch()
        {
            try
            {
                await _apiIntegrationService.FetchAndProcessExternalData();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error during automated fetch: {ex.Message}");
            }

            return Ok("Automated data fetch completed successfully.");
        }
    }
}
