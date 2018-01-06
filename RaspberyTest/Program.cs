using System;
using System.Threading;
using RaspberryLib;

namespace RaspberyTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Raspberry.Initialize();

            PinValue pinValue = PinValue.Low;

            Raspberry.SetPinDirection(Pin.PIN11_GPIO_17, PinDirection.Out);
            pinValue = Raspberry.GetPinValue(Pin.PIN11_GPIO_17);
            Console.WriteLine(Pin.PIN11_GPIO_17.ToString() + ":" + pinValue.ToString());
            Thread.Sleep(1000);

            Raspberry.SetPinValue(Pin.PIN11_GPIO_17, PinValue.High);
            pinValue = Raspberry.GetPinValue(Pin.PIN11_GPIO_17);
            Console.WriteLine(Pin.PIN11_GPIO_17.ToString() + ":" + pinValue.ToString());
            Thread.Sleep(1000);

            Raspberry.SetPinValue(Pin.PIN11_GPIO_17, PinValue.Low);
            pinValue = Raspberry.GetPinValue(Pin.PIN11_GPIO_17);
            Console.WriteLine(Pin.PIN11_GPIO_17.ToString() + ":" + pinValue.ToString());
            Thread.Sleep(1000);

            Raspberry.SetPinValue(Pin.PIN11_GPIO_17, PinValue.High);
            pinValue = Raspberry.GetPinValue(Pin.PIN11_GPIO_17);
            Console.WriteLine(Pin.PIN11_GPIO_17.ToString() + ":" + pinValue.ToString());
            Thread.Sleep(1000);

            Raspberry.SetPinValue(Pin.PIN11_GPIO_17, PinValue.Low);
            pinValue = Raspberry.GetPinValue(Pin.PIN11_GPIO_17);
            Console.WriteLine(Pin.PIN11_GPIO_17.ToString() + ":" + pinValue.ToString());
            Thread.Sleep(1000);

            Raspberry.Dispose();
        }
    }
}
