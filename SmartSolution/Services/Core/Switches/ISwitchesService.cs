using RaspberryLib;
using System;
using System.Collections.Generic;
using System.Timers;
using WebPortal.Models.Switches;

namespace WebPortal.Services.Core.Switches
{
    public interface ISwitchesService
    {
        IEnumerable<ISwitch> Switches { get; set; }
        /// <summary>
        /// Save configuration
        /// </summary>
        void SaveConfiguration();
        /// <summary>
        /// Load switches configuration from databse
        /// </summary>
        void LoadConfiguration();
        /// <summary>
        /// Gets switch by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ISwitch GetSwitchFromDatabase(string id);
        /// <summary>
        /// Gets switch by pin code
        /// </summary>
        /// <param name="pinCode"></param>
        /// <returns></returns>
        ISwitch GetSwitchFromMemory(PinCode pinCode);
        /// <summary>
        /// Get log from database
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        IEnumerable<ISwitchLog> GetSwitchesLog(DateTime from, DateTime to);
        /// <summary>
        /// Create new switch
        /// </summary>
        /// <param name="switchObject"></param>
        void CreateNew(Switch switchObject);
        /// <summary>
        /// Update switch 
        /// </summary>
        /// <param name="switchObject"></param>
        void Update(Switch switchObject);
        /// <summary>
        /// Delete switch
        /// </summary>
        /// <param name="id"></param>
        void Delete(string id);
        /// <summary>
        /// Turn on switch
        /// </summary>
        /// <param name="id"></param>
        void TurnON(string id);
        /// <summary>
        /// Turn off switch
        /// </summary>
        /// <param name="id"></param>
        void TurnOFF(string id);
        /// <summary>
        /// Get configuration
        /// </summary>
        ApplicationConfiguration Configuration { get; set; }
    }
}
