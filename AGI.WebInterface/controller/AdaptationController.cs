
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System;
using AGI.NeuralNetworkModule;
using Microsoft.Extensions.Logging;

namespace AGI.WebInterface.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdaptationController : ControllerBase
    {
        private readonly ModelTraining _modelTraining;
        private readonly ModelInference _modelInference;
        private readonly ILogger<AdaptationController> _logger;

        // Constructor uses Dependency Injection to receive ModelTraining, ModelInference instances and ILogger
        public AdaptationController(ModelTraining modelTraining, ModelInference modelInference, ILogger<AdaptationController> logger)
        {
            _modelTraining = modelTraining;
            _modelInference = modelInference;
            _logger = logger;
        }

        // Endpoint to train a model
        [HttpPost("train")]
        public IActionResult TrainModel([FromBody] TrainModelRequest request)
        {
            try
            {
                // Enhanced input validation
                if (request.Data == null || request.Labels == null)
                {
                    _logger.LogWarning("Training data or labels cannot be null.");
                    return BadRequest("Training data or labels cannot be null.");
                }

                if (request.Data.Count == 0 || request.Labels.Count == 0)
                {
                    _logger.LogWarning("Training data or labels cannot be empty.");
                    return BadRequest("Training data or labels cannot be empty.");
                }

                if (request.Data.Count != request.Labels.Count)
                {
                    _logger.LogWarning("The number of data entries must match the number of labels.");
                    return BadRequest("The number of data entries must match the number of labels.");
                }

                if (request.Epochs <= 0)
                {
                    _logger.LogWarning("Epochs must be greater than 0.");
                    return BadRequest("Epochs must be greater than 0.");
                }

                if (request.LearningRate <= 0 || request.LearningRate > 1)
                {
                    _logger.LogWarning("Learning rate must be between 0 and 1 (exclusive).");
                    return BadRequest("Learning rate must be between 0 and 1 (exclusive).");
                }

                if (string.IsNullOrEmpty(request.ModelName))
                {
                    _logger.LogWarning("Model name cannot be null or empty.");
                    return BadRequest("Model name cannot be null or empty.");
                }

                _logger.LogInformation("Starting model training for '{ModelName}' with {Epochs} epochs.", request.ModelName, request.Epochs);
                _modelTraining.TrainModel(request.Data, request.Labels, request.Epochs, request.LearningRate.ToString());
                _logger.LogInformation("Model '{ModelName}' has been trained and saved successfully.", request.ModelName);
                
                return Ok($"Model '{request.ModelName}' has been trained and saved successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during model training for '{ModelName}'", request.ModelName);
                return StatusCode(500, "An internal server error occurred while training the model.");
            }
        }

        // Endpoint to perform inference
        [HttpPost("predict")]
        public IActionResult Predict([FromBody] PredictRequest request)
        {
            try
            {
                // Enhanced input validation
                if (request.InputData == null || request.InputData.Length == 0)
                {
                    _logger.LogWarning("Input data cannot be null or empty.");
                    return BadRequest("Input data cannot be null or empty.");
                }

                if (string.IsNullOrEmpty(request.ModelName))
                {
                    _logger.LogWarning("Model name cannot be null or empty.");
                    return BadRequest("Model name cannot be null or empty.");
                }

                // Better path management and validation for model directory
                var modelDirectory = Path.Combine(AppContext.BaseDirectory, "models");
                if (!Directory.Exists(modelDirectory))
                {
                    _logger.LogWarning("The model directory '{ModelDirectory}' does not exist.", modelDirectory);
                    return NotFound("The model directory does not exist. Please make sure the models have been saved correctly.");
                }

                var modelPath = Path.Combine(modelDirectory, $"{request.ModelName}.model");
                if (!System.IO.File.Exists(modelPath))
                {
                    _logger.LogWarning("Model '{ModelName}' not found at path '{ModelPath}'.", request.ModelName, modelPath);
                    return NotFound($"Model '{request.ModelName}' not found.");
                }

                _logger.LogInformation("Loading model '{ModelName}' for prediction.", request.ModelName);
                var model = _modelInference.LoadModel(request.ModelName, modelDirectory);
                if (model == null)
                {
                    _logger.LogError("Model '{ModelName}' could not be loaded.", request.ModelName);
                    return NotFound($"Model '{request.ModelName}' could not be loaded.");
                }

                var inputDataView = _modelInference.ConvertToIDataView(request.InputData);
                var predictions = _modelInference.Predict(model, inputDataView);
                if (predictions == null || !predictions.Any())
                {
                    _logger.LogWarning("No predictions were made using model '{ModelName}'.", request.ModelName);
                    return NotFound($"No predictions were made using model '{request.ModelName}'.");
                }

                var prediction = predictions.FirstOrDefault()?.Score.FirstOrDefault() ?? -1;
                if (prediction == -1)
                {
                    _logger.LogWarning("Failed to get a prediction for model '{ModelName}'.", request.ModelName);
                    return NotFound($"Failed to get a prediction for model '{request.ModelName}'.");
                }

                _logger.LogInformation("Prediction for model '{ModelName}' was successful.", request.ModelName);
                return Ok(new { Prediction = prediction });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during prediction for model '{ModelName}'", request.ModelName);
                return StatusCode(500, "An internal server error occurred while performing the prediction.");
            }
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
