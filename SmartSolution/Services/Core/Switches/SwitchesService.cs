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
                this.timer.Elapsed += LoggSwitchesData;
                if (Configuration.LoggingEnabled)
                {
                    this.timer.Start();
                }
            }
        }

        public abstract void SaveConfiguration();
        public abstract void LoadConfiguration();
        public abstract ISwitch GetSwitchFromDatabase(string id);
        public abstract ISwitch GetSwitchFromMemory(PinCode pinCode);
        public abstract IEnumerable<ISwitchLog> GetSwitchesLog(DateTime from, DateTime to);
        public abstract void CreateNew(Switch switchObject);
        public abstract void Update(Switch switchObject);
        public abstract void Delete(string id);
        public abstract void TurnON(string id);
        public abstract void TurnOFF(string id);
        public abstract void LoggSwitchesData(object sender, ElapsedEventArgs e);
        public abstract void GenerateTestSwitches(int numOfSwitches);
        public abstract void DeleteMockupSwitches();
    }
}
