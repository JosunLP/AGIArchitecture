
using System;
using System.IO;
using System.Text.Json;

namespace AGI.NeuralNetworkModule.ModelPersistence
{
    public class ModelPersistence
    {
        private readonly string modelDirectory = "SavedModels";

        public ModelPersistence()
        {
            // Ensure the model directory exists
            if (!Directory.Exists(modelDirectory))
            {
                Directory.CreateDirectory(modelDirectory);
            }
        }

        public void SaveModel(object model, string modelName)
        {
            try
            {
                string modelPath = Path.Combine(modelDirectory, $"{modelName}.json");
                string jsonString = JsonSerializer.Serialize(model);

                File.WriteAllText(modelPath, jsonString);
                Console.WriteLine($"Model {modelName} saved successfully at {modelPath}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while saving the model: {ex.Message}");
            }
        }

        public T? LoadModel<T>(string modelName)
        {
            try
            {
                string modelPath = Path.Combine(modelDirectory, $"{modelName}.json");

                if (!File.Exists(modelPath))
                {
                    Console.WriteLine($"Model {modelName} not found.");
                    return default;
                }

                string jsonString = File.ReadAllText(modelPath);
                T model = JsonSerializer.Deserialize<T>(jsonString) ?? throw new InvalidOperationException("Deserialization returned null.");

                Console.WriteLine($"Model {modelName} loaded successfully from {modelPath}.");
                return model;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while loading the model: {ex.Message}");
                return default;
            }
        }
    }
}
