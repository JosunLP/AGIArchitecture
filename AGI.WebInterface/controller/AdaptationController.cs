
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using AGI.NeuralNetworkModule;

namespace AGI.WebInterface.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdaptationController : ControllerBase
    {
        private readonly ModelTraining _modelTraining;
        private readonly ModelInference _modelInference;

        public AdaptationController()
        {
            _modelTraining = new ModelTraining();
            _modelInference = new ModelInference();
        }

        // Endpoint to train a model
        [HttpPost("train")]
        public IActionResult TrainModel([FromBody] TrainModelRequest request)
        {
            if (request.Data == null || request.Labels == null || request.Data.Count != request.Labels.Count)
            {
                return BadRequest("Invalid training data or labels.");
            }

            _modelTraining.TrainModel(request.Data, request.Labels, request.Epochs, request.LearningRate.ToString());
            return Ok($"Model '{request.ModelName}' has been trained and saved successfully.");
        }

        // Endpoint to perform inference
        [HttpPost("predict")]
        public IActionResult Predict([FromBody] PredictRequest request)
        {
            if (request.InputData == null)
            {
                return BadRequest("Invalid input data.");
            }
            var modelDirectory = Path.Combine(AppContext.BaseDirectory, "models");
            if (string.IsNullOrEmpty(request.ModelName))
            {
                return BadRequest("Model name cannot be null or empty.");
            }
            var model = _modelInference.LoadModel(request.ModelName, modelDirectory);
            if (model == null)
            {
                return NotFound($"Model '{request.ModelName}' not found.");
            }

            var inputDataView = _modelInference.ConvertToIDataView(request.InputData);
            var predictions = _modelInference.Predict(model, inputDataView);
            if (predictions == null || !predictions.Any())
            {
                return NotFound($"Model '{request.ModelName}' not found.");
            }

            var prediction = predictions.FirstOrDefault()?.Score.FirstOrDefault() ?? -1;
            if (prediction == -1)
            {
                return NotFound($"Model '{request.ModelName}' not found.");
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
