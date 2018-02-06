using System;

namespace WebPortal.Models.Switches
{
    public class SwitchLog : ISwitchLog
    {
        public string Id { get; set; }
        public string SwitchId { get; set; }
        public bool Value { get; set; }
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Initialize logSwitch manualy, it is null
        /// </summary>
        public SwitchLog()
        {
            this.Id = Guid.NewGuid().ToString();
            this.SwitchId = "";
            this.Value = false;
            this.Timestamp = DateTime.Now;
        }

        public SwitchLog(Switch switchObj)
        {
            this.Id = Guid.NewGuid().ToString();
            this.SwitchId = switchObj.Id;
            this.Value = switchObj.State;
            this.Timestamp = DateTime.Now;
        }
    }
}
