﻿using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using WebPortal.Models.Switches;
using RaspberryLib;
using System.Timers;
using WebPortal.Services.Core.Switches;
using WebPortal.Services.Core;

namespace WebPortal.Services.SQLite
{
    public class SQLiteSwitchesService : SwitchesService
    {
        private SQLiteDbContext dbContext;

        public SQLiteSwitchesService(IDbContext dbContext, ApplicationConfiguration configuration) : base(configuration)
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

        ~SQLiteSwitchesService()
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
                    Switch switchObj = this.dbContext.Switches.Where(s => s.Id == sw.Id).SingleOrDefault();
                    if (switchObj != null)
                    {
                        switchObj.DeviceType = sw.DeviceType;
                        switchObj.Name = sw.Name;
                        switchObj.RaspberryPin = sw.RaspberryPin;
                        switchObj.SwitchType = sw.SwitchType;
                        switchObj.State = sw.State;
                        switchObj.InverseLogic = sw.InverseLogic;
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
        /// Load switches configuration from databse
        /// </summary>
        public override void LoadConfiguration()
        {
            try
            {
                List<Switch> switches = this.dbContext.Switches.ToList();
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
        public override ISwitch GetSwitchFromDatabase(string id)
        {
            ISwitch switchObj;

            try
            {
                Switch s = this.dbContext.Switches.Where(sw => sw.Id == id).SingleOrDefault();

                if (s != null)
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
                result = this.dbContext.SwitchesLog.Where(s => s.Timestamp >= from && s.Timestamp <= to);
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
                this.dbContext.Switches.Add(switchObject);
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
                Switch switchObj = this.dbContext.Switches.Where(s => s.Id == id).SingleOrDefault();
                this.dbContext.Switches.Remove(switchObj);
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

        public override void LoggSwitchesData(object sender, ElapsedEventArgs e)
        {
            try
            {
                foreach (Switch sw in Switches)
                {
                    SwitchLog switchLog = new SwitchLog(sw);
                    this.dbContext.SwitchesLog.Add(switchLog);
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

        public override void GenerateTestSwitches(int numOfSwitches)
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
                    this.dbContext.Switches.Add(mockupSwitch);
                }
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
        /// Delete all mockup switches from db. It will not delete logs for them!
        /// </summary>
        public override void DeleteMockupSwitches()
        {
            try
            {
                List<Switch> switches = this.dbContext.Switches.Where(s => s.SwitchType == SwitchType.Mockup).ToList();
                this.dbContext.RemoveRange(switches);
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
