
using System;
using System.IO;
using Microsoft.ML;

namespace AGI.NeuralNetworkModule
{
    public class ModelManagement
    {
        private readonly string _modelDirectory;
        private MLContext _mlContext;

        public ModelManagement(string modelDirectory)
        {
            _modelDirectory = modelDirectory;
            _mlContext = new MLContext();
        }

        // Laden eines vortrainierten oder gespeicherten Modells
        public ITransformer LoadModel(string modelPath)
        {
            if (!File.Exists(modelPath))
                throw new FileNotFoundException($"Model file not found: {modelPath}");

            using var fileStream = new FileStream(modelPath, FileMode.Open, FileAccess.Read, FileShare.Read);
            return _mlContext.Model.Load(fileStream, out var _);
        }

        // Speichern eines trainierten Modells
        public void SaveModel(ITransformer model, DataViewSchema modelInputSchema, string modelPath)
        {
            using var fileStream = new FileStream(modelPath, FileMode.Create, FileAccess.Write, FileShare.Write);
            _mlContext.Model.Save(model, modelInputSchema, fileStream);
        }

        // Unterstützung für verschiedene Modellformate
        public void LoadExternalModel(string format, string modelPath)
        {
            switch (format.ToLower())
            {
                case "onnx":
                    Console.WriteLine("Loading ONNX model...");
                    break;
                case "pb":
                    Console.WriteLine("Loading TensorFlow model...");
                    break;
                default:
                    throw new NotSupportedException($"Unsupported model format: {format}");
            }
        }
    }
}
