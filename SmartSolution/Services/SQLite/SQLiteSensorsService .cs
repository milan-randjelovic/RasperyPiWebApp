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
        private MongoClient client;
        private IMongoDatabase dbContext;
        private IMongoCollection<Sensor> mongoCollection;
        private IMongoCollection<SensorLog> logCollection;

        public SQLiteSensorsService() : base()
        {
            Raspberry.Initialize();
            this.client = new MongoClient(Configuration.DatabaseConnection);
            this.dbContext = client.GetDatabase(Configuration.DatabaseName);
            this.mongoCollection = dbContext.GetCollection<Sensor>(Configuration.Sensors);
            this.logCollection = dbContext.GetCollection<SensorLog>(Configuration.SensorsLog);
            this.LoadConfiguration();
            if (this.timer == null)
            {
                this.timer = new Timer(Configuration.LogInterval);
                this.timer.Start();
                this.timer.Elapsed += LogSensorsData;
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
        public override void LoadConfiguration()
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
        /// Gets sensor from databse by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override ISensor GetSensorFromDatabase(string id)
        {
            ISensor sensor;

            try
            {
                Sensor s = this.mongoCollection.Find(sw => sw.Id == id).SingleOrDefault();

                if (s != null)
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
        /// Create new sensor
        /// </summary>
        /// <param name="switchObject"></param>
        public override void CreateNew(Sensor sensor)
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
                this.mongoCollection.FindOneAndDelete(sw => sw.Id == id);
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
                    this.logCollection.InsertOne(sensorLog);
                }
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
                    mongoCollection.InsertOne(mockupSensor);
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
        /// Delete all mockup sensors from db. It will not delete logs for them!
        /// </summary>
        public override void DeleteMockupSensors()
        {
            try
            {
                this.mongoCollection.DeleteMany(sens => sens.SensorType == SensorType.Mockup);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
