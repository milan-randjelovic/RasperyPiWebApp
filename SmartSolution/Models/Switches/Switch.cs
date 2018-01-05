using RaspberryLib;
using System;
using System.ComponentModel.DataAnnotations;

namespace WebPortal.Models.Switches
{
    public class Switch : Device, ISwitch
    {
        [DataType(DataType.Text)]
        [Required(AllowEmptyStrings = false)]
        public Pin RaspberryPin { get; set; }

        [Required(AllowEmptyStrings = false)]
        public SwitchType SwitchType { get; set; }

        public bool State { get; set; }

        public Switch() : base()
        {
            this.RaspberryPin = Pin.PIN1_3V3;
            this.SwitchType = SwitchType.Regular;
            this.State = false;
        }

        public virtual void TurnON()
        {
            try
            {
                Raspberry.WriteToPin(this.RaspberryPin, PinValue.High);
                this.State = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public virtual void TurnOFF()
        {
            try
            {
                Raspberry.WriteToPin(this.RaspberryPin, PinValue.Low);
                this.State = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
    }

    public enum SwitchType
    {
        Regular,
        Mockup
    }
}
