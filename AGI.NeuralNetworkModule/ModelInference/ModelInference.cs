
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
        public void Predict(ITransformer model, IDataView inputData)
        {
            var predictions = model.Transform(inputData);
            var predictedResults = _mlContext.Data.CreateEnumerable<PredictionResult>(predictions, reuseRowObject: false);

            foreach (var prediction in predictedResults)
            {
                Console.WriteLine($"Predicted Label: {prediction.PredictedLabel}");
            }
        }

        public class PredictionResult
        {
            public float[] Score { get; set; } = [];
            public string PredictedLabel { get; set; } = string.Empty;
        }
    }
}
