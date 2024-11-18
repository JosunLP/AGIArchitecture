using System;
using System.Collections.Generic;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;

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
        public ITransformer TrainModel(List<float[]> data, List<float> labels, int epochs, string taskType)
        {
            // Combine data and labels into a single list of objects
            var trainingDataList = new List<TrainingData>();
            for (int i = 0; i < data.Count; i++)
            {
                trainingDataList.Add(new TrainingData { Features = data[i], Label = labels[i] });
            }

            // Convert the list to an IDataView
            IDataView trainingData = _mlContext.Data.LoadFromEnumerable(trainingDataList);

            IEstimator<ITransformer> pipeline = taskType.ToLower() switch
            {
                "classification" => _mlContext.Transforms.Conversion.MapValueToKey("Label")
                                        .Append(_mlContext.Transforms.Concatenate("Features", nameof(TrainingData.Features)))
                                        .Append(_mlContext.MulticlassClassification.Trainers.SdcaNonCalibrated())
                                        .Append(_mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel")),
                "regression" => _mlContext.Transforms.Concatenate("Features", nameof(TrainingData.Features))
                                        .Append(_mlContext.Regression.Trainers.Sdca()),
                _ => throw new NotSupportedException($"Unsupported task type: {taskType}"),
            };
            var model = pipeline.Fit(trainingData);
            return model;
        }

        public void PerformHyperparameterTuning(List<float[]> data, List<float> labels, string taskType)
        {
            // Combine data and labels into a single list of objects
            var trainingDataList = new List<TrainingData>();
            for (int i = 0; i < data.Count; i++)
            {
                trainingDataList.Add(new TrainingData { Features = data[i], Label = labels[i] });
            }

            // Convert the list to an IDataView
            IDataView trainingData = _mlContext.Data.LoadFromEnumerable(trainingDataList);

            // Define hyperparameter search space
            var searchSpace = new List<(string name, object[] values)>
            {
                ("L2Regularization", new object[] { 0.01f, 0.1f, 1f }),
                ("LearningRate", new object[] { 0.01f, 0.1f, 1f })
            };

            // Define pipeline
            IEstimator<ITransformer> pipeline = taskType.ToLower() switch
            {
                "classification" => _mlContext.Transforms.Conversion.MapValueToKey("Label")
                                        .Append(_mlContext.Transforms.Concatenate("Features", nameof(TrainingData.Features)))
                                        .Append(_mlContext.MulticlassClassification.Trainers.SdcaNonCalibrated()),
                "regression" => _mlContext.Transforms.Concatenate("Features", nameof(TrainingData.Features))
                                        .Append(_mlContext.Regression.Trainers.Sdca()),
                _ => throw new NotSupportedException($"Unsupported task type: {taskType}"),
            };

            // Perform grid search
            foreach (var (name, values) in searchSpace)
            {
                foreach (var value in values)
                {
                    var options = new SdcaRegressionTrainer.Options();
                    if (name == "L2Regularization") options.L2Regularization = (float)value;
                    if (name == "LearningRate") options.BiasLearningRate = (float)value;

                    var model = pipeline.Fit(trainingData);
                    var metrics = EvaluateModel(model, data, labels);

                    Console.WriteLine($"Hyperparameter: {name} = {value}, R-Squared: {metrics.RSquared}");
                }
            }
        }
        // Evaluate the model
        public RegressionMetrics EvaluateModel(ITransformer model, List<float[]> testData, List<float> testLabels)
        {
            // Combine test data and labels into a single list of objects
            var testDataList = new List<TrainingData>();
            for (int i = 0; i < testData.Count; i++)
            {
                testDataList.Add(new TrainingData { Features = testData[i], Label = testLabels[i] });
            }

            // Convert the list to an IDataView
            IDataView testDataView = _mlContext.Data.LoadFromEnumerable(testDataList);

            // Make predictions
            var predictions = model.Transform(testDataView);

            // Evaluate the model
            var metrics = _mlContext.Regression.Evaluate(predictions);

            return metrics;
        }
        // Class to hold training data
        private class TrainingData
        {
            [VectorType]
            public float[]? Features { get; set; }
            public float Label { get; set; }
        }
    }
}
