using System;
using System.ComponentModel.DataAnnotations;
using RaspberryLib;

namespace WebPortal.Models.Switches
{
    public class Switch : Device, ISwitch
    {
        [DataType(DataType.Text)]
        [Required(AllowEmptyStrings = false)]
        public PinCode RaspberryPinNumber { get; set; }

        [Required(AllowEmptyStrings = false)]
        public SwitchType SwitchType { get; set; }

        public bool State { get; set; }

        public Switch() : base()
        {
            this.RaspberryPinNumber = PinCode.NONE;
            this.SwitchType = SwitchType.Regular;
            this.State = false;
        }

        public virtual void TurnON()
        {
            try
            {
                Raspberry r = new Raspberry();
                r.WriteToPin(PinCode.PIN11_GPIO17, PinDirectiion.OUT, PinValue.HIGH);
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
                Raspberry r = new Raspberry();
                r.WriteToPin(PinCode.PIN11_GPIO17, PinDirectiion.OUT, PinValue.LOW);
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
