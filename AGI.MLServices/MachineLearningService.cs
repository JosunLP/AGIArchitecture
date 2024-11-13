
using System;
using Microsoft.ML;
using Microsoft.ML.Data;

namespace AGI.MLServices;

public class MachineLearningService
{
    private readonly MLContext _mlContext;
    private ITransformer? _model;

    public MachineLearningService()
    {
        _mlContext = new MLContext();
    }

    public void Train<TData>(IDataView trainingData)
        where TData : class
    {
        // Define a simple pipeline (e.g., a linear regression for demonstration)
        var pipeline = _mlContext.Transforms.Concatenate("Features", typeof(TData).GetProperties().Select(p => p.Name).ToArray())
            .Append(_mlContext.Regression.Trainers.Sdca());

        // Train the model
        _model = pipeline.Fit(trainingData);
    }

    public float Predict<TData>(TData input) where TData : class
    {
        if (_model == null)
            throw new InvalidOperationException("Model has not been trained.");

        var predictionEngine = _mlContext.Model.CreatePredictionEngine<TData, ModelPrediction>(_model);
        var result = predictionEngine.Predict(input);
        return result.Score;
    }
}



