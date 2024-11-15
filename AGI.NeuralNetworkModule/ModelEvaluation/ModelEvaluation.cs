
using System;
using Microsoft.ML;

namespace AGI.NeuralNetworkModule
{
    public class ModelEvaluation
    {
        private readonly MLContext _mlContext;

        public ModelEvaluation()
        {
            _mlContext = new MLContext();
        }

        // Evaluation des Modells mit spezifischen Metriken
        public void EvaluateModel(ITransformer model, IDataView testData, string taskType)
        {
            switch (taskType.ToLower())
            {
                case "classification":
                    var metrics = _mlContext.MulticlassClassification.Evaluate(model.Transform(testData));
                    Console.WriteLine($"Log-loss: {metrics.LogLoss}");
                    break;
                case "regression":
                    var regressionMetrics = _mlContext.Regression.Evaluate(model.Transform(testData));
                    Console.WriteLine($"R^2: {regressionMetrics.RSquared}");
                    break;
                default:
                    throw new NotSupportedException($"Unsupported task type: {taskType}");
            }
        }
    }
}
