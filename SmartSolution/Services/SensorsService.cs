using MongoDB.Driver;
using RaspberryLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using WebPortal.Models.Sensors;

namespace WebPortal.Services
{
    public class SensorsService
    {
        private IMongoCollection<Sensor> mongoCollection;
        private IMongoCollection<SensorLog> logCollection;
        private MongoClient client;
        private IMongoDatabase dbContext;
        private Timer timer;

        public IEnumerable<ISensor> Sensors { get; protected set; }

        public SensorsService()
        {
            Raspberry.Initialize();
            this.client = new MongoClient(Configuration.DatabaseConnection);
            this.dbContext = client.GetDatabase(Configuration.DatabaseName);
            this.mongoCollection = dbContext.GetCollection<Sensor>("Sensors");
            this.logCollection = dbContext.GetCollection<SensorLog>("SensorsLog");
            this.LoadConfiguration();
            if (this.timer == null)
            {
                this.timer = new Timer(Configuration.LogInterval);
                this.timer.Start();
                this.timer.Elapsed += LogSensorsData;
            }
        }

        private void LogSensorsData(object sender, ElapsedEventArgs e)
        {
            try
            {
                foreach (Sensor sens in Sensors)
                {
                    SensorLog sensorLog = new SensorLog(sens);
                    this.logCollection.InsertOne(sensorLog);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ~SensorsService()
        {
            Raspberry.Dispose();
        }

        /// <summary>
        /// Save configuration
        /// </summary>
        public void SaveConfiguration()
        {
            try
            {
                foreach (Sensor sensor in Sensors)
                {
                    this.mongoCollection.FindOneAndReplace(s => s.Id == sensor.Id, sensor);
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
        public void LoadConfiguration()
        {
            try
            {
                List<Sensor> sensors = this.mongoCollection.Find(s => s.Id != "").ToList();
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
        /// Gets sensor by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ISensor Find(string id)
        {
            ISensor sensor;

            try
            {
                Sensor s = this.mongoCollection.Find(sw => sw.Id == id).SingleOrDefault();
                if (s.SensorType == SensorType.Mockup)
                {

                    MockupSensor mockupSensor = new MockupSensor();
                    mockupSensor.DeviceType = s.DeviceType;
                    mockupSensor.Id = s.Id;
                    mockupSensor.Name = s.Name;
                    mockupSensor.SensorType = s.SensorType;
                    mockupSensor.Timestamp = s.Timestamp;
                    mockupSensor.Value = s.Value;
                    sensor = mockupSensor;
                }
                else
                {
                    sensor = s;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return sensor;
        }

        /// <summary>
        /// Create new sensor
        /// </summary>
        /// <param name="switchObject"></param>
        public void CreateNew(Sensor sensor)
        {
            try
            {
                this.mongoCollection.InsertOne(sensor);
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
        public void Update(Sensor sensor)
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
        public void Delete(string id)
        {
            try
            {
                this.mongoCollection.FindOneAndDelete(sw => sw.Id == id);
                this.SaveConfiguration();
                this.LoadConfiguration();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
