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
                raspberry.ReleaseAll();
                raspberry.SelectPin(this.RaspberryPinNumber);
                raspberry.WriteToPin(GpioPinState.High);
                this.State = raspberry.ReadFromPin(this.RaspberryPinNumber) == GpioPinState.High ? true : false;
                Console.WriteLine(raspberry.ReadFromPin(this.RaspberryPinNumber));
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
                GpioManager raspberry = new GpioManager();
                raspberry.ReleaseAll();
                raspberry.SelectPin(this.RaspberryPinNumber);
                raspberry.WriteToPin(GpioPinState.Low);
                this.State = raspberry.ReadFromPin(this.RaspberryPinNumber) == GpioPinState.Low ? true : false;
                Console.WriteLine(raspberry.ReadFromPin(this.RaspberryPinNumber));
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
