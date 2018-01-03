using System;
using System.Collections.Generic;
using System.Text;

namespace RaspberryLib
{
    public class Pin
    {
        public PinCode Code { get; set; }
        public PinDirectiion Direction { get; set; }
        public PinValue Value { get; set; }
        public int Number
        {
            get
            {
                return (int)this.Code;
            }
        }
        public string Name
        {
            get
            {
                return Enum.GetName(typeof(PinCode), (PinCode)this.Number);
            }
        }
   
        public Pin()
        {
            this.Code = PinCode.NONE;
            this.Direction = PinDirectiion.UNKNOWN;
            this.Value = PinValue.UNKNOWN;
        }
    }
}
