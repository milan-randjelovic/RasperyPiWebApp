using MongoDB.Driver;
using RaspberryLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using WebPortal.Models.Sensors;
using WebPortal.Services.Core;

namespace WebPortal.Services.SQLite
{
    public class SQLiteSensorsService : SensorsService
    {
        private SQLiteDatabase dbContext;

        public SQLiteSensorsService() : base()
        {
            Raspberry.Initialize();
            this.dbContext = new SQLiteDatabase(Configuration.DatabaseConnection);
            this.dbContext.Database.EnsureCreated();
            this.LoadConfiguration();
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
                foreach (Sensor sensor in Sensors)
                {
                    Sensor s = this.dbContext.Sensors.Find(sensor.Id);
                    s.DeviceType = sensor.DeviceType;
                    s.Name = sensor.Name;
                    s.RaspberryPin = sensor.RaspberryPin;
                    s.SensorType = sensor.SensorType;
                    s.Timestamp = sensor.Timestamp;
                    s.Value = sensor.Value;
                }
                this.dbContext.SaveChanges();
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
        /// Create new sensor
        /// </summary>
        /// <param name="switchObject"></param>
        public override void CreateNew(Sensor sensor)
        {
            try
            {
                this.dbContext.Sensors.Add(sensor);
                this.dbContext.SaveChanges();
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
                Sensor sensor = this.dbContext.Sensors.Where(s => s.Id == id).SingleOrDefault();
                this.dbContext.Sensors.Remove(sensor);
                this.dbContext.SaveChanges();
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
                    this.dbContext.SensorsLog.Add(sensorLog);
                }
                this.dbContext.SaveChanges();
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
                this.dbContext.SaveChanges();
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
                this.dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
