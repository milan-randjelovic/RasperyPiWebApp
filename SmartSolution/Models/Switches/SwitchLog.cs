using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebPortal.Models.Switches
{
    public class SwitchLog
    {
        public string Id { get; set; }
        public Switch logSwitch { get; set; }
        public DateTime Timestamp { get; set; }


        /// <summary>
        /// Initialize logSwitch manualy, it is null
        /// </summary>
        public SwitchLog()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Timestamp = DateTime.Now;
        }


        public SwitchLog(Switch switchObj)
        {
            this.Id = Guid.NewGuid().ToString();
            this.Timestamp = DateTime.Now;
            this.logSwitch = switchObj;
        }
    }
}
