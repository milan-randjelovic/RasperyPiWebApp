using System.Threading;
using RaspberryLib;

namespace RaspberyTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Raspberry.Initialize();

            Raspberry.WriteToPin(Pin.PIN11_GPIO_17, PinValue.High);
            Thread.Sleep(1000);
            Raspberry.WriteToPin(Pin.PIN11_GPIO_17, PinValue.Low);
            Thread.Sleep(1000);
            Raspberry.WriteToPin(Pin.PIN11_GPIO_17, PinValue.High);
            Thread.Sleep(1000);
            Raspberry.WriteToPin(Pin.PIN11_GPIO_17, PinValue.Low);
            Thread.Sleep(1000);
            Raspberry.Dispose();
        }
    }
}
