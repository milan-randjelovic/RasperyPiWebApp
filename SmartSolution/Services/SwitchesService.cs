using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using WebPortal.Models.Switches;
using RaspberryLib;

namespace WebPortal.Services
{
    public class SwitchesService
    {
        private MongoClient client;
        private IMongoDatabase dbContext;
        private IMongoCollection<Switch> mongoCollection;
        public IEnumerable<ISwitch> Switches { get; protected set; }

        public SwitchesService()
        {
            Raspberry.Initialize();
            this.client = new MongoClient(Configuration.DatabaseConnection);
            this.dbContext = client.GetDatabase(Configuration.DatabaseName);
            this.mongoCollection = dbContext.GetCollection<Switch>("Switches");
            this.Switches = new List<Switch>();
            this.LoadConfiguration();
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
    }
}
