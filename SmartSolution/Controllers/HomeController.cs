using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RaspberryLib;
using Test.Models;
using WebPortal.Models;
using WebPortal.Models.Sensors;
using WebPortal.Models.Switches;
using WebPortal.Services;

namespace Test.Controllers
{
    public class HomeController : Controller
    {
        protected static SensorsService SensorsService { get; private set; }
        protected static SwitchesService SwitchesService { get; private set; }

        public HomeController(SwitchesService switchesService, SensorsService sensorsService)
        {
            SwitchesService = switchesService;

            if (SwitchesService == null)
            {
                SwitchesService = new SwitchesService();
            }

            SensorsService = sensorsService;

            if (SensorsService == null)
            {
                SensorsService = new SensorsService();
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetPinData(string pinCode)
        {
            PinCode code = EnumeratorHelpers.GetPinCode(pinCode);

            ISensor se = SensorsService.GetSensorFromMemory(code);
            if (se != null)
            {
                return Json(se);
            }

            ISwitch sw = SwitchesService.GetSwitchFromMemory(code);
            if (sw != null)
            {
                return Json(sw);
            }

            return Json(null);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
