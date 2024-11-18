namespace AGI.WebInterface.Controller;


// Request classes for model training and inference
public class TrainModelRequest
{
    public List<float[]>? Data { get; set; }
    public List<float>? Labels { get; set; }
    public int Epochs { get; set; }
    public float LearningRate { get; set; }
    public string? ModelName { get; set; }
    public string? ModelType { get; set; }  // Added ModelType to support multiple model types
}
