
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AGI.APIIntegration;
using Newtonsoft.Json.Linq;

namespace AGI.WebInterface.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExternalDataController : ControllerBase
    {
        private readonly APIIntegrationService _apiIntegrationService;

        public ExternalDataController(APIIntegrationService apiIntegrationService)
        {
            _apiIntegrationService = apiIntegrationService;
        }

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
    }
}
