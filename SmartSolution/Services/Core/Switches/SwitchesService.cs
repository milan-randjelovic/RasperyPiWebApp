using System;
using System.Collections.Generic;
using System.Timers;
using RaspberryLib;
using WebPortal.Models.Switches;

namespace WebPortal.Services.Core.Switches
{
    public abstract class SwitchesService : ISwitchesService
    {
        protected Timer timer;
        public ApplicationConfiguration Configuration { get; set; }
        public IEnumerable<ISwitch> Switches { get; set; }

        public SwitchesService(ApplicationConfiguration configuration)
        {
            this.Configuration = configuration;
            if (this.timer == null)
            {
                this.timer = new Timer(Configuration.LoggingInterval);
                this.timer.Elapsed += LogSwitchesData;              
            }
        }

        public abstract void SaveConfiguration();
        /// <summary>
        /// Load switches configuration from databse
        /// </summary>
        public abstract void LoadConfiguration();
        /// <summary>
        /// Gets switch by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public abstract ISwitch GetSwitchFromDatabase(string id);
        /// <summary>
        /// Gets switch by pin code
        /// </summary>
        /// <param name="pinCode"></param>
        /// <returns></returns>
        public abstract ISwitch GetSwitchFromMemory(PinCode pinCode);
        /// <summary>
        /// Get log from database
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public abstract IEnumerable<ISwitchLog> GetSwitchesLog(DateTime from, DateTime to);
        /// <summary>
        /// Create new switch
        /// </summary>
        /// <param name="switchObject"></param>
        public abstract void CreateNew(Switch switchObject);
        /// <summary>
        /// Update switch 
        /// </summary>
        /// <param name="switchObject"></param>
        public abstract void Update(Switch switchObject);
        /// <summary>
        /// Delete switch
        /// </summary>
        /// <param name="id"></param>
        public abstract void Delete(string id);
        /// <summary>
        /// Turn on switch
        /// </summary>
        /// <param name="id"></param>
        public abstract void TurnON(string id);
        /// <summary>
        /// Turn off switch
        /// </summary>
        /// <param name="id"></param>
        public abstract void TurnOFF(string id);
        /// <summary>
        /// Log switch data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public abstract void LogSwitchesData(object sender, ElapsedEventArgs e);
    }
}
