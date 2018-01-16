using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebPortal.Models.Sensors
{
    public class SensorLog
    {
        string Id { get; set; }
        public Sensor LogSensor { get; set; }
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Initialize LogSensor manualy, it is null
        /// </summary>
        public SensorLog()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Timestamp = DateTime.Now;
        }

        public SensorLog(Sensor sensor)
        {
            this.LogSensor = sensor;
            this.Id = Guid.NewGuid().ToString();
            this.Timestamp = DateTime.Now;
        }
    }
}
