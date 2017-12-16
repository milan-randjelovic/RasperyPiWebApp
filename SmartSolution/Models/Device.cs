using System.Collections.Generic;

namespace WebPortal.Models
{
    public class Device
    {
        public List<Sensor> Sensors { get; set; }

        public Device()
        {
            this.Sensors = new List<Sensor>();
        }
    }
}