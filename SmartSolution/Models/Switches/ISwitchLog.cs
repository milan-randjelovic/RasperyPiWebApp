using System;

namespace WebPortal.Models.Switches
{
    public interface ISwitchLog
    {
        string Id { get; set; }
        string SwitchId { get; set; }
        bool Value { get; set; }
        DateTime Timestamp { get; set; }
    }
}
