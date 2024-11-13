
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace AGI.APIIntegration
{
    public class APIIntegrationService
    {
        private readonly HttpClient _httpClient;

        public APIIntegrationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Example method to fetch data from an external API (e.g., weather or news)
        public async Task<JObject> FetchExternalDataAsync(string requestUrl)
        {
            var response = await _httpClient.GetAsync(requestUrl);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JObject.Parse(content); // Parses the response as JSON
        }
    }
}
