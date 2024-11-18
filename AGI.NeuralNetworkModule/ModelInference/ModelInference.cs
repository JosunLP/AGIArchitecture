
using System;
using Microsoft.ML;

namespace AGI.NeuralNetworkModule
{
    public class ModelInference
    {
        private readonly MLContext _mlContext;

        public ModelInference()
        {
            _mlContext = new MLContext();
        }

        // Vorhersagen mit einem geladenen Modell
        public IEnumerable<PredictionResult>? Predict(ITransformer model, IDataView inputData)
        {
            var predictions = model.Transform(inputData);
            var predictedResults = _mlContext.Data.CreateEnumerable<PredictionResult>(predictions, reuseRowObject: false);

            foreach (var prediction in predictedResults)
            {
                Console.WriteLine($"Predicted Label: {prediction.PredictedLabel}");
            }

            return predictedResults;
        }

        public IEnumerable<PredictionResult> GetPredictions(ITransformer model, IDataView inputData)
        {
            var predictions = model.Transform(inputData);
            var predictedResults = _mlContext.Data.CreateEnumerable<PredictionResult>(predictions, reuseRowObject: false);
            return predictedResults;
        }

        public class PredictionResult
        {
            public float[] Score { get; set; } = [];
            public string PredictedLabel { get; set; } = string.Empty;
        }


        public ITransformer? LoadModel(string modelName, string modelDirectory)
        {
            try
            {
                string modelPath = Path.Combine(modelDirectory, $"{modelName}.zip");

                if (!File.Exists(modelPath))
                {
                    Console.WriteLine($"Model {modelName} not found.");
                    return null;
                }

                ITransformer model = _mlContext.Model.Load(modelPath, out var modelSchema);
                Console.WriteLine($"Model {modelName} loaded successfully from {modelPath}.");
                return model;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while loading the model: {ex.Message}");
                return default;
            }
        }

        public IDataView ConvertToIDataView(float[] inputData)
        {
            var data = new List<InputData>();
            data.Add(new InputData { Features = inputData });
            return _mlContext.Data.LoadFromEnumerable(data);
        }

        public class InputData
        {
            public float[] Features { get; set; } = [];
        }
    }
}
