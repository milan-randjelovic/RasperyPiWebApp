﻿using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using WebPortal.Models.Switches;
using RaspberryLib;
using System.Timers;
using WebPortal.Services.Core.Switches;

namespace WebPortal.Services.Mongo
{
    public class MongoSwitchesService : SwitchesService
    {
        private MongoDbContext dbContext;

        public MongoSwitchesService(IMongoDbContext dbContext, ApplicationConfiguration configuration) : base(configuration)
        {
            Raspberry.Initialize();
            this.dbContext = (MongoDbContext)dbContext;
            this.LoadConfiguration();
            if (Configuration.LoggingEnabled)
            {
                this.timer.Start();
            }
        }

        ~MongoSwitchesService()
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
                foreach (Switch sw in Switches)
                {
                    this.dbContext.Switches.FindOneAndReplace(s => s.Id == sw.Id, sw);
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
        public override void LoadConfiguration()
        {
            try
            {
                List<Switch> switches = this.dbContext.Switches.Find(s => s.Id != "").ToList();
                List<Switch> result = new List<Switch>();

                foreach (Switch s in switches)
                {
                    if (s.SwitchType == SwitchType.Mockup)
                    {
                        MockupSwitch mockupSwitch = new MockupSwitch
                        {
                            DeviceType = s.DeviceType,
                            Id = s.Id,
                            Name = s.Name,
                            RaspberryPin = s.RaspberryPin,
                            State = s.State,
                            SwitchType = s.SwitchType,
                            InverseLogic = s.InverseLogic
                        };
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
        public override ISwitch GetSwitchFromDatabase(string id)
        {
            ISwitch switchObj;

            try
            {
                Switch s = this.dbContext.Switches.Find(sw => sw.Id == id).SingleOrDefault();

                if (s != null)
                {
                    if (s.SwitchType == SwitchType.Mockup)
                    {
                        MockupSwitch mockupSwitch = new MockupSwitch
                        {
                            DeviceType = s.DeviceType,
                            Id = s.Id,
                            Name = s.Name,
                            RaspberryPin = s.RaspberryPin,
                            State = s.State,
                            SwitchType = s.SwitchType
                        };
                        switchObj = mockupSwitch;
                    }
                }

                switchObj = s;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return switchObj;
        }
        /// <summary>
        /// Gets switch by pin code
        /// </summary>
        /// <param name="pinCode"></param>
        /// <returns></returns>
        public override ISwitch GetSwitchFromMemory(PinCode pinCode)
        {
            ISwitch switchObj = Switches.Where(s => s.RaspberryPin == pinCode).FirstOrDefault();
            return switchObj;
        }

        /// <summary>
        /// Get switches log
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public override IEnumerable<ISwitchLog> GetSwitchesLog(DateTime from, DateTime to)
        {
            try
            {
                IEnumerable<ISwitchLog> result = new List<ISwitchLog>();
                result = this.dbContext.SwitchesLog.Find(s => s.Timestamp >= from && s.Timestamp <= to).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Create new switch
        /// </summary>
        /// <param name="switchObject"></param>
        public override void CreateNew(Switch switchObject)
        {
            try
            {
                this.dbContext.Switches.InsertOne(switchObject);
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
        public override void Update(Switch switchObject)
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
        public override void Delete(string id)
        {
            try
            {
                this.dbContext.Switches.FindOneAndDelete(sw => sw.Id == id);
                this.SaveConfiguration();
                this.LoadConfiguration();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override void TurnON(string id)
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

        public override void TurnOFF(string id)
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

        public override void LogSwitchesData(object sender, ElapsedEventArgs e)
        {
            try
            {
                foreach (Switch sw in Switches)
                {
                    SwitchLog switchLog = new SwitchLog(sw);
                    this.dbContext.SwitchesLog.InsertOne(switchLog);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
