
using System;
using Microsoft.ML;
using Microsoft.ML.Data;

namespace AGI.NeuralNetworkModule
{
    public class ModelTraining
    {
        private readonly MLContext _mlContext;

        public ModelTraining()
        {
            _mlContext = new MLContext();
        }

        // Trainingspipeline zum Trainieren eines Modells auf eigenen Daten
        public ITransformer TrainModel(IDataView trainingData, string taskType)
        {
            IEstimator<ITransformer> pipeline;

            switch (taskType.ToLower())
            {
                case "classification":
                    pipeline = _mlContext.Transforms.Conversion.MapValueToKey("Label")
                        .Append(_mlContext.Transforms.Text.FeaturizeText("Features", "TextColumn"))
                        .Append(_mlContext.MulticlassClassification.Trainers.SdcaNonCalibrated())
                        .Append(_mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));
                    break;
                case "regression":
                    pipeline = _mlContext.Transforms.Concatenate("Features", "FeatureColumns")
                        .Append(_mlContext.Regression.Trainers.Sdca());
                    break;
                default:
                    throw new NotSupportedException($"Unsupported task type: {taskType}");
            }

            var model = pipeline.Fit(trainingData);
            return model;
        }

        // Hyperparameter-Tuning (optional)
        public void PerformHyperparameterTuning()
        {
            Console.WriteLine("Hyperparameter tuning not implemented yet.");
        }
    }
}
