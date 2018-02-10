using System;

namespace WebPortal.Models.Sensors
{
    public class SensorLog : ISensorLog
    {
        public string Id { get; set; }
        public string SensorId { get; set; }
        public string Value { get; set; }
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Create new instance of SensorLog
        /// </summary>
        public SensorLog()
        {
            this.Id = Guid.NewGuid().ToString();
            this.SensorId = "";
            this.Value = "";
            this.Timestamp = DateTime.Now;
        }

        /// <summary>
        /// Create new instance of SensorLog based on sensor
        /// </summary>
        /// <param name="sensor"></param>
        public SensorLog(Sensor sensor)
        {
            this.Id = Guid.NewGuid().ToString();
            this.SensorId = sensor.Id;
            this.Value = sensor.Value;
            this.Timestamp = DateTime.Now;
        }
    }
}
