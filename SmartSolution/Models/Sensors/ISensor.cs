using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebPortal.Models.Sensors
{
    public interface ISensor
    {
        string Id { get; set; }
        string Name { get; set; }
        string Vendor { get; set; }
        string Model { get; set; }
        string Value { get; set; }
        DateTime Timestamp { get; set; }
        bool IsActive { get; set; }
    }
}
