
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AGI.WebInterface.Tests
{
    public class AdaptationControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public AdaptationControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task TrainModel_ReturnsOkResult()
        {
            // Arrange
            var request = new
            {
                Data = new float[][] { new float[] { 1.0f, 2.0f }, new float[] { 3.0f, 4.0f } },
                Labels = new float[] { 0.5f, 1.5f },
                Epochs = 10,
                LearningRate = 0.01f,
                ModelName = "TestModel"
            };

            var jsonContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/adaptation/train", jsonContent);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("TestModel has been trained", responseString);
        }

        [Fact]
        public async Task Predict_ReturnsPredictionResult()
        {
            // Arrange
            var request = new
            {
                InputData = new float[] { 1.0f, 2.0f },
                ModelName = "TestModel"
            };

            var jsonContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/adaptation/predict", jsonContent);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Prediction", responseString);
        }
    }
}
