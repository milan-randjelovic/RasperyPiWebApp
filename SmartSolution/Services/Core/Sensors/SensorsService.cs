using System;
using System.Collections.Generic;
using System.Timers;
using RaspberryLib;
using WebPortal.Models.Sensors;

namespace WebPortal.Services.Core.Sensors
{
    public abstract class SensorsService : ISensorsService
    {
        protected Timer timer;
        public ApplicationConfiguration Configuration { get; set; }
        public IEnumerable<ISensor> Sensors { get; set; }

        public SensorsService(ApplicationConfiguration configuration)
        {
            this.Configuration = configuration;
            if (this.timer == null)
            {
                this.timer = new Timer(Configuration.LoggingInterval);
                this.timer.Elapsed += LogSensorsData;                
            }
        }

        /// <summary>
        /// Save configuration
        /// </summary>
        public abstract void SaveConfiguration();
        /// <summary>
        /// Load sensors configuration from databse
        /// </summary>
        public abstract void LoadConfiguration();
        /// <summary>
        /// Gets sensor from databse by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public abstract ISensor GetSensorFromDatabase(string id);
        /// <summary>
        /// Gets sensor by pin code
        /// </summary>
        /// <param name="pinCode"></param>
        /// <returns></returns>
        public abstract ISensor GetSensorFromMemory(PinCode pinCode);
        /// <summary>
        /// Get log from database
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public abstract IEnumerable<ISensorLog> GetSensorsLog(DateTime from, DateTime to);
        /// <summary>
        /// Create new sensor
        /// </summary>
        /// <param name="switchObject"></param>
        public abstract void CreateNew(Sensor sensor);
        /// <summary>
        /// Update sensor 
        /// </summary>
        /// <param name="switchObject"></param>
        public abstract void Update(Sensor sensor);
        /// <summary>
        /// Delete sensor
        /// </summary>
        /// <param name="id"></param>
        public abstract void Delete(string id);
        /// <summary>
        /// Log sensor data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public abstract void LogSensorsData(object sender, ElapsedEventArgs e);
    }
}
