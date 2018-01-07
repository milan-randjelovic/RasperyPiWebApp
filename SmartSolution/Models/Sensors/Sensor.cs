using System;
using System.ComponentModel.DataAnnotations;
using RaspberryLib;

namespace WebPortal.Models.Sensors
{
    public class Sensor : Device, ISensor
    {
        [DataType(DataType.Text)]
        [Required(AllowEmptyStrings = false)]
        public PinCode RaspberryPin { get; set; }

        public SensorType SensorType { get; set; }

        [DataType(DataType.Text)]
        public string Value { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Timestamp { get; set; }

        public Sensor() : base()
        {
            this.RaspberryPin = PinCode.PIN1_3V3;
            this.SensorType = SensorType.Regular;
            this.Value = "";
            this.Timestamp = DateTime.Now;
        }
    }

    public enum SensorType
    {
        Regular,
        Mockup
    }
}