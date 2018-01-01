using System;
using System.ComponentModel.DataAnnotations;

namespace WebPortal.Models.Sensors
{
    public class Sensor : Device, ISensor
    {
        [DataType(DataType.Text)]
        [Required(AllowEmptyStrings = false)]
        public string Vendor { get; set; }

        [DataType(DataType.Text)]
        [Required(AllowEmptyStrings = false)]
        public string Model { get; set; }

        [DataType(DataType.Text)]
        public string Value { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Timestamp { get; set; }

        public Sensor() : base()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Name = "";
            this.Value = "";
            this.Timestamp = DateTime.Now;
            this.Vendor = "";
            this.Model = "";
        }
    }
}