using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RaspberryLib;
using Test.Models;
using WebPortal.Models;
using WebPortal.Models.Sensors;
using WebPortal.Models.Switches;
using WebPortal.Services.Core.Sensors;
using WebPortal.Services.Core.Switches;

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
            //Check if user is logged
            //if it is not-redirect to Login page whitch contains signIn button
            return RedirectToAction("Login");
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

        /// <summary>
        /// Show sing in page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SignIn(User user)
        {
            return View();
        }

        /// <summary>
        /// Show login page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Login user
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Login(User user)
        {
            return View();
        }
    }
}
