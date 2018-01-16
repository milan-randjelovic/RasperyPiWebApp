using RaspberryLib;
using System;
using System.ComponentModel.DataAnnotations;

namespace WebPortal.Models.Switches
{
    public class Switch : Device, ISwitch
    {
        [DataType(DataType.Text)]
        [Required(AllowEmptyStrings = false)]
        public PinCode RaspberryPin { get; set; }

        [Required(AllowEmptyStrings = false)]
        public SwitchType SwitchType { get; set; }

        public bool State { get; set; }

        public bool InverseLogic { get; set; }

        public Switch() : base()
        {
            this.RaspberryPin = PinCode.PIN1_3V3;
            this.SwitchType = SwitchType.Regular;
            this.State = false;
            this.InverseLogic = false;
        }

        public virtual void TurnON()
        {
            try
            {
                if (this.InverseLogic == false)
                {
                    Raspberry.SetPinDirection(this.RaspberryPin, PinDirection.Out);
                    Raspberry.SetPinValue(this.RaspberryPin, PinValue.High);
                    PinValue pinValue = Raspberry.GetPinValue(this.RaspberryPin);
                    this.State = (int)pinValue > 0;
                }
                else
                {
                    Raspberry.SetPinDirection(this.RaspberryPin, PinDirection.Out);
                    Raspberry.SetPinValue(this.RaspberryPin, PinValue.Low);
                    PinValue pinValue = Raspberry.GetPinValue(this.RaspberryPin);
                    this.State = (int)pinValue > 0;
                    this.State = !State;
                }

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
                if (this.InverseLogic == false)
                {
                    Raspberry.SetPinDirection(this.RaspberryPin, PinDirection.Out);
                    Raspberry.SetPinValue(this.RaspberryPin, PinValue.Low);
                    PinValue pinValue = Raspberry.GetPinValue(this.RaspberryPin);
                    this.State = (int)pinValue > 0;
                }
                else
                {
                    Raspberry.SetPinDirection(this.RaspberryPin, PinDirection.Out);
                    Raspberry.SetPinValue(this.RaspberryPin, PinValue.High);
                    PinValue pinValue = Raspberry.GetPinValue(this.RaspberryPin);
                    this.State = (int)pinValue > 0;
                    this.State = !State;
                }
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
