using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using WebPortal.Models.Switches;
using RaspberryLib;
using System.Timers;

namespace WebPortal.Services
{
    public class SwitchesService
    {
        private MongoClient client;
        private IMongoDatabase dbContext;
        private IMongoCollection<Switch> mongoCollection;
        private IMongoCollection<SwitchLog> logCollection;
        public IEnumerable<ISwitch> Switches { get; protected set; }
        private Timer timer;

        public SwitchesService()
        {
            Raspberry.Initialize();
            this.client = new MongoClient(Configuration.DatabaseConnection);
            this.dbContext = client.GetDatabase(Configuration.DatabaseName);
            this.mongoCollection = dbContext.GetCollection<Switch>(Configuration.SwitchesCollection);
            this.logCollection = dbContext.GetCollection<SwitchLog>(Configuration.SwitchesLogCollection);
            this.Switches = new List<Switch>();
            this.LoadConfiguration();
            if (this.timer == null)
            {
                this.timer = new Timer(Configuration.LogInterval);
                this.timer.Start();
                this.timer.Elapsed += LoggSwitchesData;
            }
        }

        private void LoggSwitchesData(object sender, ElapsedEventArgs e)
        {
            try
            {
                foreach (Switch sw in Switches)
                {
                    SwitchLog switchLog = new SwitchLog(sw);
                    this.logCollection.InsertOne(switchLog);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ~SwitchesService()
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
                foreach (Switch sw in Switches)
                {
                    this.mongoCollection.FindOneAndReplace(s => s.Id == sw.Id, sw);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Load switches configuration from databse
        /// </summary>
        public void LoadConfiguration()
        {
            try
            {
                List<Switch> switches = this.mongoCollection.Find(s => s.Id != "").ToList();
                List<Switch> result = new List<Switch>();

                foreach (Switch s in switches)
                {
                    if (s.SwitchType == SwitchType.Mockup)
                    {
                        MockupSwitch mockupSwitch = new MockupSwitch();
                        mockupSwitch.DeviceType = s.DeviceType;
                        mockupSwitch.Id = s.Id;
                        mockupSwitch.Name = s.Name;
                        mockupSwitch.RaspberryPin = s.RaspberryPin;
                        mockupSwitch.State = s.State;
                        mockupSwitch.SwitchType = s.SwitchType;
                        mockupSwitch.InverseLogic = s.InverseLogic;
                        result.Add(mockupSwitch);
                    }
                    else
                    {
                        result.Add(s);
                    }
                }

                this.Switches = result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets switch by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ISwitch Find(string id)
        {
            ISwitch switchObj;

            try
            {
                Switch s = this.mongoCollection.Find(sw => sw.Id == id).SingleOrDefault();
                if (s.SwitchType == SwitchType.Mockup)
                {
                    MockupSwitch mockupSwitch = new MockupSwitch();
                    mockupSwitch.DeviceType = s.DeviceType;
                    mockupSwitch.Id = s.Id;
                    mockupSwitch.Name = s.Name;
                    mockupSwitch.RaspberryPin = s.RaspberryPin;
                    mockupSwitch.State = s.State;
                    mockupSwitch.SwitchType = s.SwitchType;
                    switchObj = mockupSwitch;
                }
                else
                {
                    switchObj = s;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return switchObj;
        }

        /// <summary>
        /// Create new switch
        /// </summary>
        /// <param name="switchObject"></param>
        public void CreateNew(Switch switchObject)
        {
            try
            {
                this.mongoCollection.InsertOne(switchObject);
                this.SaveConfiguration();
                this.LoadConfiguration();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Update switch 
        /// </summary>
        /// <param name="switchObject"></param>
        public void Update(Switch switchObject)
        {
            try
            {
                ISwitch s = this.Switches.Where(sw => sw.Id == switchObject.Id).SingleOrDefault();
                s.Name = switchObject.Name;
                s.RaspberryPin = switchObject.RaspberryPin;
                s.InverseLogic = switchObject.InverseLogic;
                s.State = switchObject.State;
                s.SwitchType = switchObject.SwitchType;
                this.SaveConfiguration();
                this.LoadConfiguration();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Delete switch
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

        public void TurnON(string id)
        {
            try
            {
                ISwitch sw = this.Switches.Where(s => s.Id == id).FirstOrDefault();
                sw.TurnON();
                this.SaveConfiguration();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void TurnOFF(string id)
        {
            try
            {
                ISwitch sw = this.Switches.Where(s => s.Id == id).FirstOrDefault();
                sw.TurnOFF();
                this.SaveConfiguration();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GenerateTestSwitches(int numOfSwitches)
        {
            if (numOfSwitches > 20)
            {
                numOfSwitches = 20;
            }
            if (numOfSwitches < 0)
            {
                numOfSwitches = 0;
            }

            try
            {
                for (int i = 0; i < numOfSwitches; i++)
                {
                    MockupSwitch mockupSwitch = new MockupSwitch();
                    mockupSwitch.Name = "TestSensor";
                    mongoCollection.InsertOne(mockupSwitch);
                }
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
