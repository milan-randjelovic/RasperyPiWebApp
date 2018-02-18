using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaspberryLib;
using Test.Models;
using WebPortal.Models.Sensors;
using WebPortal.Models.Switches;
using WebPortal.Services.Core.Sensors;
using WebPortal.Services.Core.Switches;

namespace Test.Controllers
{
    [Authorize]
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

        /// <summary>
        /// Get data for pin
        /// </summary>
        /// <param name="pinCode"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Show error page
        /// </summary>
        /// <returns></returns>
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
