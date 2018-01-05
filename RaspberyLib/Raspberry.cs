using System;
using System.Collections.Generic;
using System.Linq;

namespace RaspberryLib
{
    public static class Raspberry
    {
        public static List<Pin> Pins { get; set; }

        public static void Initialize()
        {
            Export(Pin.PIN11_GPIO_17);
        }

        public static void Dispose()
        {
            Unexport(Pin.PIN11_GPIO_17);
        }

        public static void WriteToPin(Pin pin, PinValue pinValue)
        {
            SetDirection(pin, PinDirection.Out);
            SetValue(pin, pinValue);
        }

        private static void Export(Pin pin)
        {
            try
            {
                string pinAddress = pin.ToString().Split('_').Last().Replace("0", "");
                ("echo " + pinAddress + " > /sys/class/gpio/export").Bash();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void SetDirection(Pin pin, PinDirection pinDirection)
        {
            try
            {
                string pinAddress = pin.ToString().Split('_').Last().Replace("0", "");
                string direction = "";
                switch (pinDirection)
                {
                    case PinDirection.In:
                        direction = "in";
                        break;
                    case PinDirection.Out:
                        direction = "out";
                        break;
                }
                ("echo " + direction + " > /sys/class/gpio/gpio" + pinAddress + "/direction").Bash();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void SetValue(Pin pin, PinValue pinValue)
        {
            try
            {
                string pinAddress = pin.ToString().Split('_').Last().Replace("0", "");
                string value = "";
                switch (pinValue)
                {
                    case PinValue.High:
                        value = "1";
                        break;
                    case PinValue.Low:
                        value = "0";
                        break;
                }
                ("echo " + value + " > /sys/class/gpio/gpio" + pinAddress + "/value").Bash();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void Unexport(Pin pin)
        {
            try
            {
                string pinAddress = pin.ToString().Split('_').Last().Replace("0", "");
                ("echo " + pinAddress + " > /sys/class/gpio/unexport").Bash();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
