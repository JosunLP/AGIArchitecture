using System;
using AGI.Common;
using AGI.DataManagement;
using AGI.Adaptation;

namespace AGI.Sensors;

public class TemperatureSensor : ISensor
{
    private readonly DataManager _dataManager;
    private readonly AdaptationModule _adaptationModule;

    public string Name => "Temperature Sensor";
    public string Type => "Environment";

    public TemperatureSensor(DataManager dataManager, AdaptationModule adaptationModule)
    {
        _dataManager = dataManager;
        _adaptationModule = adaptationModule;
    }

    public void Initialize()
    {
        Console.WriteLine($"{Name} initialized using {_adaptationModule.GetType().Name}.");
    }

    public SensorData ReadData()
    {
        // Simulate reading temperature data
        var data = new SensorData
        {
            Timestamp = DateTime.Now,
            Data = "Temperature: 22.5°C"
        };

        // Save the data using the DataManager
        _dataManager.SaveSensorData(data);

        return data;
    }
}
