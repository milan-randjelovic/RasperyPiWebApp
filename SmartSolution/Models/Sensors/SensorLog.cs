using System;

namespace WebPortal.Models.Sensors
{
    public class SensorLog
    {
        public string Id { get; set; }
        public string SensorId { get; set; }
        public string Value { get; set; }
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Initialize LogSensor manualy, it is null
        /// </summary>
        public SensorLog()
        {
            this.Id = Guid.NewGuid().ToString();
            this.SensorId = "";
            this.Value = "";
            this.Timestamp = DateTime.Now;
        }

        public SensorLog(Sensor sensor)
        {
            this.Id = Guid.NewGuid().ToString();
            this.SensorId = sensor.Id;
            this.Value = sensor.Value;
            this.Timestamp = DateTime.Now;
        }
    }
}
