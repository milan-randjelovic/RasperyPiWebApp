using System;
using System.ComponentModel.DataAnnotations;
using PiOfThings.GpioCore;
using PiOfThings.GpioUtils;

namespace WebPortal.Models.Switches
{
    public class Switch : Device, ISwitch
    {
        [DataType(DataType.Text)]
        [Required(AllowEmptyStrings = false)]
        public GpioId RaspberryPinNumber { get; set; }

        [Required(AllowEmptyStrings = false)]
        public SwitchType SwitchType { get; set; }

        public bool State { get; set; }

        public Switch() : base()
        {
            this.RaspberryPinNumber = GpioId.GPIOUnknown;
            this.SwitchType = SwitchType.Regular;
            this.State = false;
        }

        public virtual void TurnON()
        {
            try
            {
                GpioManager raspberry = new GpioManager();
                raspberry.SelectPin(this.RaspberryPinNumber);
                raspberry.WriteToPin(GpioPinState.High);
                this.State = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual void TurnOFF()
        {
            try
            {
                GpioManager raspberry = new GpioManager();
                raspberry.SelectPin(this.RaspberryPinNumber);
                raspberry.WriteToPin(GpioPinState.High);
                this.State = false;
            }
            catch (Exception ex)
            {
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
