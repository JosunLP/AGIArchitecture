using Microsoft.ML.Data;

namespace AGI.MLServices;

public class ModelPrediction
{
    [ColumnName("Score")]
    public float Score { get; set; }
}
