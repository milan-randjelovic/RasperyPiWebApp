using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
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
                "sudo - i".Bash();
                "echo \"17\" > /sys/class/gpio/export".Bash();
                "echo \"out\" > /sys/class/gpio/gpio17/direction".Bash();
                "echo \"1\" > /sys/class/gpio/gpio17/value".Bash();

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
                "sudo - i".Bash();
                "echo \"17\" > /sys/class/gpio/export".Bash();
                "echo \"out\" > /sys/class/gpio/gpio17/direction".Bash();
                "echo \"0\" > /sys/class/gpio/gpio17/value".Bash();

                this.State = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public virtual void Block()
        {
            this.State = false;
            this.IsActive = false;
        }
    }

    public enum SwitchType
    {
        Regular,
        Mockup
    }
}
