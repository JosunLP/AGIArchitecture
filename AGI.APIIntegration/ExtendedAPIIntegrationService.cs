using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace AGI.APIIntegration
{
    public class ExtendedAPIIntegrationService(HttpClient httpClient)
    {
        private readonly HttpClient _httpClient = httpClient;

        // Fetch external data from a given API endpoint
        public async Task<JObject> FetchExternalDataAsync(string requestUrl)
        {
            var response = await _httpClient.GetAsync(requestUrl);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JObject.Parse(content); // Parse the response as JSON
        }

        // Example method to fetch financial data
        public async Task<JObject> FetchFinancialDataAsync(string stockSymbol)
        {
            var requestUrl = $"https://api.example.com/stock/{stockSymbol}"; // Replace with an actual stock API
            return await FetchExternalDataAsync(requestUrl);
        }

        // Example method to fetch weather data
        public async Task<JObject> FetchWeatherDataAsync(string city)
        {
            var requestUrl = $"https://api.weather.com/v1/weather/{city}"; // Replace with an actual weather API
            return await FetchExternalDataAsync(requestUrl);
        }

        // Method for automated data retrieval for specific tasks (e.g., periodic data fetching)
        public async Task FetchAndProcessExternalData()
        {
            // Example for periodic financial data fetch
            var financialData = await FetchFinancialDataAsync("AAPL"); // Example: Apple's stock data
            ProcessFinancialData(financialData);

            // Example for periodic weather data fetch
            var weatherData = await FetchWeatherDataAsync("Berlin"); // Example: Berlin weather
            ProcessWeatherData(weatherData);
        }

        // Method to process financial data (e.g., passing to ML models, reasoning)
        private void ProcessFinancialData(JObject financialData)
        {
            // Process financial data for ML or reasoning (placeholder)
            Console.WriteLine("Processing financial data...");
        }

        // Method to process weather data (e.g., passing to ML models, reasoning)
        private void ProcessWeatherData(JObject weatherData)
        {
            // Process weather data for ML or reasoning (placeholder)
            Console.WriteLine("Processing weather data...");
        }

        public void RunIntegration()
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\nChoose an option:");
                Console.WriteLine("1. Fetch Financial Data");
                Console.WriteLine("2. Fetch Weather Data");
                Console.WriteLine("3. Automated Data Retrieval");
                Console.WriteLine("4. Exit");
                Console.Write("Enter your choice: ");

                string? choice = Console.ReadLine();
                if (choice == null)
                {
                    Console.WriteLine("Invalid input. Please try again.");
                    continue;
                }
                switch (choice)
                {
                    case "1":
                        Console.Write("Enter stock symbol (e.g., AAPL): ");
                        string? stockSymbol = Console.ReadLine();
                        if (string.IsNullOrEmpty(stockSymbol))
                        {
                            Console.WriteLine("Invalid input. Please try again.");
                            continue;
                        }
                        var financialData = FetchFinancialDataAsync(stockSymbol).Result;
                        Console.WriteLine(financialData);
                        break;
                    case "2":
                        Console.Write("Enter city (e.g., Berlin): ");
                        string? city = Console.ReadLine();
                        if (string.IsNullOrEmpty(city))
                        {
                            Console.WriteLine("Invalid input. Please try again.");
                            continue;
                        }
                        var weatherData = FetchWeatherDataAsync(city).Result;
                        Console.WriteLine(weatherData);
                        break;
                    case "3":
                        FetchAndProcessExternalData().Wait();
                        break;
                    case "4":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
}
