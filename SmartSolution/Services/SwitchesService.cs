using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPortal.Models.Switches;

namespace WebPortal.Services
{
    public class SwitchesService
    {
        public IEnumerable<ISwitch> Switches;

        public SwitchesService()
        {
        }
    }
}
