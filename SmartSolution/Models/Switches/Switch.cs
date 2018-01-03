using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Threading;
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
                raspberry.ReleaseAll();
                raspberry.SelectPin(this.RaspberryPinNumber);
                raspberry.WriteToPin(GpioPinState.High);
                this.State = raspberry.ReadFromPin(this.RaspberryPinNumber) == GpioPinState.High ? true : false;
                raspberry.ReleaseAll();
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
                raspberry.ReleaseAll();
                raspberry.SelectPin(this.RaspberryPinNumber);
                raspberry.WriteToPin(GpioPinState.Low);
                this.State = raspberry.ReadFromPin(this.RaspberryPinNumber) == GpioPinState.High ? true : false;
                raspberry.ReleaseAll();
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
