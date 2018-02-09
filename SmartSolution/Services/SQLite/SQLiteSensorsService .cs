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
        private SQLiteDbContext dbContext;

        public SQLiteSensorsService(IDbContext dbContext, ApplicationConfiguration configuration) : base(configuration)
        {
            Raspberry.Initialize();
            this.dbContext = (SQLiteDbContext)dbContext;
            this.dbContext.Database.EnsureCreated();
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
                foreach (Sensor sens in Sensors)
                {
                    Sensor sensor = this.dbContext.Sensors.Where(s => s.Id == sens.Id).SingleOrDefault();
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
                this.timer.Stop();
                this.timer.Enabled = false;
                this.dbContext.SaveChanges();
                this.timer.Enabled = true;
                this.timer.Start();
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
                List<Sensor> sensors = this.dbContext.Sensors.ToList();
                List<Sensor> result = new List<Sensor>();

                foreach (Sensor s in sensors)
                {
                    if (s.SensorType == SensorType.Mockup)
                    {
                        MockupSensor mockupSensor = new MockupSensor();
                        mockupSensor.DeviceType = s.DeviceType;
                        mockupSensor.Id = s.Id;
                        mockupSensor.Name = s.Name;
                        mockupSensor.SensorType = s.SensorType;
                        mockupSensor.RaspberryPin = s.RaspberryPin;
                        mockupSensor.Timestamp = s.Timestamp;
                        mockupSensor.Value = s.Value;
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
                Sensor sens = this.dbContext.Sensors.Where(s => s.Id == id).SingleOrDefault();

                if (sens != null)
                {
                    if (sens.SensorType == SensorType.Mockup)
                    {

                        MockupSensor mockupSensor = new MockupSensor();
                        mockupSensor.DeviceType = sens.DeviceType;
                        mockupSensor.Id = sens.Id;
                        mockupSensor.Name = sens.Name;
                        mockupSensor.SensorType = sens.SensorType;
                        mockupSensor.RaspberryPin = sens.RaspberryPin;
                        mockupSensor.Timestamp = sens.Timestamp;
                        mockupSensor.Value = sens.Value;
                        sensor = mockupSensor;
                    }
                }

                sensor = sens;
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
                IEnumerable<ISensorLog> result = new List<SensorLog>();
                result = this.dbContext.SensorsLog.Where(s => s.Timestamp >= from && s.Timestamp <= to);
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
                this.dbContext.Sensors.Add(sensor);
                this.timer.Stop();
                this.timer.Enabled = false;
                this.dbContext.SaveChanges();
                this.timer.Enabled = true;
                this.timer.Start(); this.SaveConfiguration();
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
                Sensor sensor = this.dbContext.Sensors.Where(s => s.Id == id).SingleOrDefault();
                this.dbContext.Sensors.Remove(sensor);
                this.timer.Stop();
                this.timer.Enabled = false;
                this.dbContext.SaveChanges();
                this.timer.Enabled = true;
                this.timer.Start(); this.SaveConfiguration();
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
                    this.dbContext.SensorsLog.Add(sensorLog);
                }
                this.timer.Stop();
                this.timer.Enabled = false;
                this.dbContext.SaveChanges();
                this.timer.Enabled = true;
                this.timer.Start();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Create test sensors in database
        /// </summary>
        /// <param name="numOfSensors"> User define this(maximum is 20)</param>
        public override void GenerateTestSensors(int numOfSensors)
        {
            if (numOfSensors > 20)
            {
                numOfSensors = 20;
            }
            if (numOfSensors < 0)
            {
                numOfSensors = 0;
            }

            try
            {
                for (int i = 0; i < numOfSensors; i++)
                {
                    MockupSensor mockupSensor = new MockupSensor();
                    mockupSensor.Name = "TestSensor";
                    this.dbContext.Sensors.Add(mockupSensor);
                }
                this.timer.Stop();
                this.timer.Enabled = false;
                this.dbContext.SaveChanges();
                this.timer.Enabled = true;
                this.timer.Start();
                this.SaveConfiguration();
                this.LoadConfiguration();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Delete all mockup sensors from db. It will not delete logs for them!
        /// </summary>
        public override void DeleteMockupSensors()
        {
            try
            {
                List<Sensor> sensors = this.dbContext.Sensors.Where(sens => sens.SensorType == SensorType.Mockup).ToList();
                this.dbContext.RemoveRange(sensors);
                this.timer.Stop();
                this.timer.Enabled = false;
                this.dbContext.SaveChanges();
                this.timer.Enabled = true;
                this.timer.Start();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
