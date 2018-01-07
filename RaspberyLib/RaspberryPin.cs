using System;
using System.Collections.Generic;
using System.Text;

namespace RaspberryLib
{
    public class Pin
    {
        public PinCode PinCode { get; set; }
        public PinDirection PinDirection { get; set; }
        public PinValue PinValue { get; set; }
        public bool Initialized { get; set; }
        public PinType PinType { get; }

        public Pin(PinCode pinCode)
        {
            this.PinCode = pinCode;
            
            if (pinCode.ToString().Contains("GPIO"))
            {
                this.PinType = PinType.GPIO;
                this.PinDirection = PinDirection.Out;
                this.PinValue = PinValue.Low;
            }
            else
            if (pinCode.ToString().Contains("GND"))
            {
                this.PinType = PinType.GND;
                this.PinDirection = PinDirection.Out;
                this.PinValue = PinValue.Low;
            }
            else
            if (pinCode.ToString().Contains("3V") || pinCode.ToString().Contains("5V"))
            {
                this.PinType = PinType.POWER;
                this.PinDirection = PinDirection.Out;
                this.PinValue = PinValue.High;
            }
        }

    }
}
