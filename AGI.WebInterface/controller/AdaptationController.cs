
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using AGI.NeuralNetworkModule;

namespace AGI.WebInterface.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdaptationController : ControllerBase
    {
        private readonly ModelTraining _modelTraining;
        private readonly ModelInference _modelInference;

        // Constructor uses Dependency Injection to receive ModelTraining and ModelInference instances
        public AdaptationController(ModelTraining modelTraining, ModelInference modelInference)
        {
            _modelTraining = modelTraining;
            _modelInference = modelInference;
        }

        // Endpoint to train a model
        [HttpPost("train")]
        public IActionResult TrainModel([FromBody] TrainModelRequest request)
        {
            // Enhanced input validation
            if (request.Data == null || request.Labels == null)
            {
                return BadRequest("Training data or labels cannot be null.");
            }

            if (request.Data.Count == 0 || request.Labels.Count == 0)
            {
                return BadRequest("Training data or labels cannot be empty.");
            }

            if (request.Data.Count != request.Labels.Count)
            {
                return BadRequest("The number of data entries must match the number of labels.");
            }

            if (request.Epochs <= 0)
            {
                return BadRequest("Epochs must be greater than 0.");
            }

            if (request.LearningRate <= 0 || request.LearningRate > 1)
            {
                return BadRequest("Learning rate must be between 0 and 1 (exclusive).");
            }

            if (string.IsNullOrEmpty(request.ModelName))
            {
                return BadRequest("Model name cannot be null or empty.");
            }

            _modelTraining.TrainModel(request.Data, request.Labels, request.Epochs, request.LearningRate.ToString());
            return Ok($"Model '{request.ModelName}' has been trained and saved successfully.");
        }

        // Endpoint to perform inference
        [HttpPost("predict")]
        public IActionResult Predict([FromBody] PredictRequest request)
        {
            // Enhanced input validation
            if (request.InputData == null || request.InputData.Length == 0)
            {
                return BadRequest("Input data cannot be null or empty.");
            }

            if (string.IsNullOrEmpty(request.ModelName))
            {
                return BadRequest("Model name cannot be null or empty.");
            }

            // Better path management and validation for model directory
            var modelDirectory = Path.Combine(AppContext.BaseDirectory, "models");
            if (!Directory.Exists(modelDirectory))
            {
                return NotFound("The model directory does not exist. Please make sure the models have been saved correctly.");
            }

            var modelPath = Path.Combine(modelDirectory, $"{request.ModelName}.model");
            if (!System.IO.File.Exists(modelPath))
            {
                return NotFound($"Model '{request.ModelName}' not found at path '{modelPath}'.");
            }

            var model = _modelInference.LoadModel(request.ModelName, modelDirectory);
            if (model == null)
            {
                return NotFound($"Model '{request.ModelName}' could not be loaded.");
            }

            var inputDataView = _modelInference.ConvertToIDataView(request.InputData);
            var predictions = _modelInference.Predict(model, inputDataView);
            if (predictions == null || !predictions.Any())
            {
                return NotFound($"No predictions were made using model '{request.ModelName}'.");
            }

            var prediction = predictions.FirstOrDefault()?.Score.FirstOrDefault() ?? -1;
            if (prediction == -1)
            {
                return NotFound($"Failed to get a prediction for model '{request.ModelName}'.");
            }

            return Ok(new { Prediction = prediction });
        }
    }

    // Request classes for model training and inference
    public class TrainModelRequest
    {
        public List<float[]>? Data { get; set; }
        public List<float>? Labels { get; set; }
        public int Epochs { get; set; }
        public float LearningRate { get; set; }
        public string? ModelName { get; set; }
    }

    public class PredictRequest
    {
        public float[]? InputData { get; set; }
        public string? ModelName { get; set; }
    }
}
