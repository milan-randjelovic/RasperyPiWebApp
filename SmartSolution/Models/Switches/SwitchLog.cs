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
        /// Create new instance of SwitchLog
        /// </summary>
        public SwitchLog()
        {
            this.Id = Guid.NewGuid().ToString();
            this.SwitchId = "";
            this.Value = false;
            this.Timestamp = DateTime.Now;
        }

        /// <summary>
        /// Create new instance of SwitchLog based on switchd
        /// </summary>
        /// <param name="switchObj"></param>
        public SwitchLog(Switch switchObj)
        {
            this.Id = Guid.NewGuid().ToString();
            this.SwitchId = switchObj.Id;
            this.Value = switchObj.State;
            this.Timestamp = DateTime.Now;
        }
    }
}
