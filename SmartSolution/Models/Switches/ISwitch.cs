using PiOfThings.GpioUtils;

namespace WebPortal.Models.Switches
{
    public interface ISwitch
    {
        string Id { get; set; }
        string Name { get; set; }
        SwitchType SwitchType { get; set; }
        bool State { get; set; }
        GpioId RaspberryPinNumber { get; set; }
        bool IsActive { get; set; }
        void TurnON();
        void TurnOFF();
    }
}
