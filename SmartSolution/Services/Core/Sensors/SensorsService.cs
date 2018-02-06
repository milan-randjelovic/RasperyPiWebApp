using System.Collections.Generic;
using System.Timers;
using RaspberryLib;
using WebPortal.Models.Sensors;

namespace WebPortal.Services.Core.Sensors
{
    public abstract class SensorsService : ISensorsService
    {
        protected Timer timer;
        public IEnumerable<ISensor> Sensors { get; set; }
        public bool LoggingEnabled
        {
            get
            {
                return this.timer.Enabled;
            }
            set
            {
                this.timer.Enabled = value;
            }
        }

        public SensorsService()
        {
            if (this.timer == null)
            {
                this.timer = new Timer(Configuration.LogInterval);
                this.timer.Elapsed += LogSensorsData;
                if (Configuration.LoggingEnabled)
                {
                    this.timer.Start();
                }
            }
        }

        public abstract void SaveConfiguration();
        public abstract void LoadConfiguration();
        public abstract ISensor GetSensorFromDatabase(string id);
        public abstract ISensor GetSensorFromMemory(PinCode pinCode);
        public abstract void CreateNew(Sensor sensor);
        public abstract void Update(Sensor sensor);
        public abstract void Delete(string id);
        public abstract void LogSensorsData(object sender, ElapsedEventArgs e);
        public abstract void GenerateTestSensors(int numOfSensors);
        public abstract void DeleteMockupSensors();
    }
}
