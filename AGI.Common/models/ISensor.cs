using System;

namespace AGI.Common;

public interface ISensor
{
    string Name { get; }
    string Type { get; }
    void Initialize();
    SensorData ReadData();
}

public class SensorData
{
    public DateTime Timestamp { get; set; }
    public required string Data { get; set; }

    public override string ToString()
    {
        return $"{Timestamp}: {Data}";
    }
}
