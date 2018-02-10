using MongoDB.Driver;
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
        public SQLiteSwitchesService(ISQLiteDbContext dbContext, ApplicationConfiguration configuration) : base(configuration)
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
                using (SQLiteDbContext dbContext = new SQLiteDbContext(this.Configuration))
                {
                    foreach (Switch sw in Switches)
                    {
                        Switch switchObj = dbContext.Switches.Where(s => s.Id == sw.Id).SingleOrDefault();
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
                    dbContext.SaveChanges();
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
                using (SQLiteDbContext dbContext = new SQLiteDbContext(this.Configuration))
                {
                    List<Switch> switches = dbContext.Switches.ToList();
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
                using (SQLiteDbContext dbContext = new SQLiteDbContext(this.Configuration))
                {
                    Switch s = dbContext.Switches.Where(sw => sw.Id == id).SingleOrDefault();

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
                using (SQLiteDbContext dbContext = new SQLiteDbContext(this.Configuration))
                {
                    IEnumerable<ISwitchLog> result = new List<ISwitchLog>();
                    result = dbContext.SwitchesLog.Where(s => s.Timestamp >= from && s.Timestamp <= to);
                    return result;
                }
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
                using (SQLiteDbContext dbContext = new SQLiteDbContext(this.Configuration))
                {
                    dbContext.Switches.Add(switchObject);
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
                using (SQLiteDbContext dbContext = new SQLiteDbContext(this.Configuration))
                {
                    Switch switchObj = dbContext.Switches.Where(s => s.Id == id).SingleOrDefault();
                    dbContext.Switches.Remove(switchObj);
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
                using (SQLiteDbContext dbContext = new SQLiteDbContext(this.Configuration))
                {
                    foreach (Switch sw in Switches)
                    {
                        SwitchLog switchLog = new SwitchLog(sw);
                        dbContext.SwitchesLog.Add(switchLog);
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
