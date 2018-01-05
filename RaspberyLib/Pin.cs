using System;
using System.Collections.Generic;
using System.Text;

namespace RaspberryLib
{
    public class RaspberryPin
    {
        public Pin Pin { get; set; }
        public PinDirection PinDirection { get; set; }
        public PinValue PinValue { get; set; }
    }
}
