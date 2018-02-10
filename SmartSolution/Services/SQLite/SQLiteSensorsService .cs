using MongoDB.Driver;
using RaspberryLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using WebPortal.Models.Sensors;
using WebPortal.Services.Core;
using WebPortal.Services.Core.Sensors;

namespace WebPortal.Services.SQLite
{
    public class SQLiteSensorsService : SensorsService
    {
        public SQLiteSensorsService(ISQLiteDbContext dbContext, ApplicationConfiguration configuration) : base(configuration)
        {
            Raspberry.Initialize();
            using (SQLiteDbContext context = new SQLiteDbContext(this.Configuration))
            {
                context.Database.EnsureCreated();
            }
            this.LoadConfiguration();
            if (Configuration.LoggingEnabled)
            {
                this.timer.Start();
            }
        }

        ~SQLiteSensorsService()
        {
            Raspberry.Dispose();
        }

        /// <summary>
        /// Save configuration
        /// </summary>
        public override void SaveConfiguration()
        {
            try
            {
                using (SQLiteDbContext dbContext = new SQLiteDbContext(this.Configuration))
                {
                    foreach (Sensor sens in Sensors)
                    {
                        Sensor sensor = dbContext.Sensors.Where(s => s.Id == sens.Id).SingleOrDefault();
                        if (sensor != null)
                        {
                            sensor.DeviceType = sens.DeviceType;
                            sensor.Name = sens.Name;
                            sensor.RaspberryPin = sens.RaspberryPin;
                            sensor.SensorType = sens.SensorType;
                            sensor.Timestamp = sens.Timestamp;
                            sensor.Value = sens.Value;
                        }
                    }
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Load sensors configuration from databse
        /// </summary>
        public override void LoadConfiguration()
        {
            try
            {
                using (SQLiteDbContext dbContext = new SQLiteDbContext(this.Configuration))
                {
                    List<Sensor> sensors = dbContext.Sensors.ToList();
                    List<Sensor> result = new List<Sensor>();

                    foreach (Sensor s in sensors)
                    {
                        if (s.SensorType == SensorType.Mockup)
                        {
                            MockupSensor mockupSensor = new MockupSensor
                            {
                                DeviceType = s.DeviceType,
                                Id = s.Id,
                                Name = s.Name,
                                SensorType = s.SensorType,
                                RaspberryPin = s.RaspberryPin,
                                Timestamp = s.Timestamp,
                                Value = s.Value
                            };
                            result.Add(mockupSensor);
                        }
                        else
                        {
                            result.Add(s);
                        }
                    }

                    this.Sensors = result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets sensor from databse by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override ISensor GetSensorFromDatabase(string id)
        {
            ISensor sensor;

            try
            {
                using (SQLiteDbContext dbContext = new SQLiteDbContext(this.Configuration))
                {
                    Sensor sens = dbContext.Sensors.Where(s => s.Id == id).SingleOrDefault();

                    if (sens != null)
                    {
                        if (sens.SensorType == SensorType.Mockup)
                        {

                            MockupSensor mockupSensor = new MockupSensor
                            {
                                DeviceType = sens.DeviceType,
                                Id = sens.Id,
                                Name = sens.Name,
                                SensorType = sens.SensorType,
                                RaspberryPin = sens.RaspberryPin,
                                Timestamp = sens.Timestamp,
                                Value = sens.Value
                            };
                            sensor = mockupSensor;
                        }
                    }

                    sensor = sens;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return sensor;
        }
        /// <summary>
        /// Gets sensor by pin code
        /// </summary>
        /// <param name="pinCode"></param>
        /// <returns></returns>
        public override ISensor GetSensorFromMemory(PinCode pinCode)
        {
            ISensor sensor = Sensors.Where(s => s.RaspberryPin == pinCode).FirstOrDefault();
            return sensor;
        }

        /// <summary>
        /// Get sensos log
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public override IEnumerable<ISensorLog> GetSensorsLog(DateTime from, DateTime to)
        {
            try
            {
                using (SQLiteDbContext dbContext = new SQLiteDbContext(this.Configuration))
                {
                    IEnumerable<ISensorLog> result = new List<SensorLog>();
                    result = dbContext.SensorsLog.Where(s => s.Timestamp >= from && s.Timestamp <= to);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Create new sensor
        /// </summary>
        /// <param name="switchObject"></param>
        public override void CreateNew(Sensor sensor)
        {
            try
            {
                using (SQLiteDbContext dbContext = new SQLiteDbContext(this.Configuration))
                {
                    dbContext.Sensors.Add(sensor);
                    dbContext.SaveChanges();
                }
                this.SaveConfiguration();
                this.LoadConfiguration();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Update sensor 
        /// </summary>
        /// <param name="switchObject"></param>
        public override void Update(Sensor sensor)
        {
            try
            {
                ISensor se = this.Sensors.Where(s => s.Id == sensor.Id).SingleOrDefault();
                se.Name = sensor.Name;
                se.RaspberryPin = sensor.RaspberryPin;
                se.SensorType = sensor.SensorType;
                se.Timestamp = sensor.Timestamp;
                se.Value = sensor.Value;
                this.SaveConfiguration();
                this.LoadConfiguration();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Delete sensor
        /// </summary>
        /// <param name="id"></param>
        public override void Delete(string id)
        {
            try
            {
                using (SQLiteDbContext dbContext = new SQLiteDbContext(this.Configuration))
                {
                    Sensor sensor = dbContext.Sensors.Where(s => s.Id == id).SingleOrDefault();
                    dbContext.Sensors.Remove(sensor);
                    dbContext.SaveChanges();
                }
                this.SaveConfiguration();
                this.LoadConfiguration();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Log sensors data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void LogSensorsData(object sender, ElapsedEventArgs e)
        {
            try
            {
                using (SQLiteDbContext dbContext = new SQLiteDbContext(this.Configuration))
                {
                    foreach (Sensor sens in Sensors)
                    {
                        SensorLog sensorLog = new SensorLog(sens);
                        dbContext.SensorsLog.Add(sensorLog);
                    }
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
