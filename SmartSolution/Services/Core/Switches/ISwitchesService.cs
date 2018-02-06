using RaspberryLib;
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
        /// Enables/Disables logging
        /// </summary>
        bool LoggingEnabled { get; set; }
        /// <summary>
        /// Generate test switches
        /// </summary>
        /// <param name="numOfSwitches"></param>
        void GenerateTestSwitches(int numOfSwitches);
        /// <summary>
        /// Delete all mockup switches from db. It will not delete logs for them!
        /// </summary>
        void DeleteMockupSwitches();
    }
}
