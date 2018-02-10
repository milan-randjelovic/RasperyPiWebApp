using MongoDB.Driver;
using RaspberryLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using WebPortal.Models.Sensors;
using WebPortal.Services.Core;
using WebPortal.Services.Core.Sensors;

namespace WebPortal.Services.Mongo
{
    public class MongoSensorsService : SensorsService
    {
        private MongoDbContext dbContext;

        public MongoSensorsService(IDbContext dbContext, ApplicationConfiguration configuration) : base(configuration)
        {
            Raspberry.Initialize();
            this.dbContext = (MongoDbContext)dbContext;
            this.LoadConfiguration();
            if (Configuration.LoggingEnabled)
            {
                this.timer.Start();
            }
        }

        ~MongoSensorsService()
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
                foreach (Sensor sensor in Sensors)
                {
                    this.dbContext.Sensors.FindOneAndReplace(s => s.Id == sensor.Id, sensor);
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
                List<Sensor> sensors = this.dbContext.Sensors.Find(s => s.Id != "").ToList();
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
                Sensor s = this.dbContext.Sensors.Find(sw => sw.Id == id).SingleOrDefault();

                if (s != null)
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
                        sensor = mockupSensor;
                    }
                }

                sensor = s;
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
        /// Get sensors log
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public override IEnumerable<ISensorLog> GetSensorsLog(DateTime from, DateTime to)
        {
            try
            {
                IEnumerable<ISensorLog> result = new List<ISensorLog>();
                result = this.dbContext.SensorsLog.Find(s => s.Timestamp >= from && s.Timestamp <= to).ToList();
                return result;
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
                this.dbContext.Sensors.InsertOne(sensor);
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
                this.dbContext.Sensors.FindOneAndDelete(sw => sw.Id == id);
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
                foreach (Sensor sens in Sensors)
                {
                    SensorLog sensorLog = new SensorLog(sens);
                    this.dbContext.SensorsLog.InsertOne(sensorLog);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
