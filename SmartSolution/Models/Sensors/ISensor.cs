using RaspberryLib;
using System;

namespace WebPortal.Models.Sensors
{
    public interface ISensor
    {
        string Id { get; set; }
        string Name { get; set; }
        SensorType SensorType { get; set; }
        string Value { get; set; }
        PinCode RaspberryPin { get; set; }
        DateTime Timestamp { get; set; }
    }
}
