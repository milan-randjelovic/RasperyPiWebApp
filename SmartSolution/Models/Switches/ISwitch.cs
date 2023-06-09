﻿using RaspberryLib;

namespace WebPortal.Models.Switches
{
    public interface ISwitch
    {
        string Id { get; set; }
        string Name { get; set; }
        SwitchType SwitchType { get; set; }
        bool State { get; set; }
        bool InverseLogic { get; set; }
        PinCode RaspberryPin { get; set; }
        void TurnON();
        void TurnOFF();
    }
}
