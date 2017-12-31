using System.ComponentModel.DataAnnotations;
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
            this.State = true;
        }

        public virtual void TurnOFF()
        {
            this.State = false;
        }
    }

    public enum SwitchType
    {
        Regular,
        Mockup
    }
}
