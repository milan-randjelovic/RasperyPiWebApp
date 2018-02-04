using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RaspberryLib;
using Test.Models;
using WebPortal.Models.Sensors;
using WebPortal.Models.Switches;
using WebPortal.Services.Core;
using WebPortal.Services.Mongo;

namespace Test.Controllers
{
    public class HomeController : Controller
    {
        protected static ISwitchesService SwitchesService { get; private set; }
        protected static ISensorsService SensorsService { get; private set; }

        public HomeController(ISwitchesService switchesService, ISensorsService sensorsService)
        {
            SwitchesService = switchesService;
            SensorsService = sensorsService;
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
