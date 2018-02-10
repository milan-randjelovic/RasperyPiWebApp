using RaspberryLib;
using System;
using System.Collections.Generic;
using WebPortal.Models.Sensors;

namespace WebPortal.Services.Core.Sensors
{
    public interface ISensorsService
    {
        IEnumerable<ISensor> Sensors { get; set; }
        /// <summary>
        /// Save configuration
        /// </summary>
        void SaveConfiguration();
        /// <summary>
        /// Load sensors configuration from databse
        /// </summary>
        void LoadConfiguration();
        /// <summary>
        /// Gets sensor from databse by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ISensor GetSensorFromDatabase(string id);
        /// <summary>
        /// Gets sensor by pin code
        /// </summary>
        /// <param name="pinCode"></param>
        /// <returns></returns>
        ISensor GetSensorFromMemory(PinCode pinCode);
        /// <summary>
        /// Get log from database
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        IEnumerable<ISensorLog> GetSensorsLog(DateTime from, DateTime to);
        /// <summary>
        /// Create new sensor
        /// </summary>
        /// <param name="switchObject"></param>
        void CreateNew(Sensor sensor);
        /// <summary>
        /// Update sensor 
        /// </summary>
        /// <param name="switchObject"></param>
        void Update(Sensor sensor);
        /// <summary>
        /// Delete sensor
        /// </summary>
        /// <param name="id"></param>
        void Delete(string id);
        /// <summary>
        /// Get configuration
        /// </summary>
        ApplicationConfiguration Configuration { get; set; }
    }
}
