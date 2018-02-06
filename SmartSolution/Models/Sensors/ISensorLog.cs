using System;

namespace WebPortal.Models.Sensors
{
    public interface ISensorLog
    {
        string Id { get; set; }
        string SensorId { get; set; }
        string Value { get; set; }
        DateTime Timestamp { get; set; }
    }
}
