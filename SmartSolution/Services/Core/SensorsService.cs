using System.Collections.Generic;
using System.Timers;
using RaspberryLib;
using WebPortal.Models.Sensors;

namespace WebPortal.Services.Core
{
    public abstract class SensorsService : ISensorsService
    {
        protected Timer timer;
        public IEnumerable<ISensor> Sensors { get; set; }

        public SensorsService()
        {
            if (this.timer == null)
            {
                this.timer = new Timer(Configuration.LogInterval);
                this.timer.Start();
                this.timer.Elapsed += LogSensorsData;
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
