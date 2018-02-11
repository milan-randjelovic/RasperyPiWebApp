using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaspberryLib;
using RestSharp;
using SmartSolutionAPILib;
using Test.Models;
using WebPortal.Models;
using WebPortal.Models.Sensors;
using WebPortal.Models.Switches;
using WebPortal.Services.Core.Sensors;
using WebPortal.Services.Core.Switches;
using WebPortal.Services.Core.Users;

namespace Test.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        protected static ISwitchesService SwitchesService { get; private set; }
        protected static ISensorsService SensorsService { get; private set; }
        protected static IUsersService UsersService { get; private set; }

        public HomeController(ISwitchesService switchesService, ISensorsService sensorsService, IUsersService usersService)
        {
            SwitchesService = switchesService;
            SensorsService = sensorsService;
            UsersService = usersService;
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

        /// <summary>
        /// Show sing in page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SignUp(User user)
        {
            if (!ModelState.IsValid) {
                return View();
            }
            try
            {
                IRestResponse response = SmartSolutionAPI.Post(UsersService.configuration.APIBaseAddress, UsersService.configuration.Users, user.Id, user);
                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    //User is created
                    return RedirectToAction("SignIn","Home",null);
                }
                else {
                    //Bad request
                    return View();
                }
            }
            catch(Exception ex) {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Error", "Home", null);
            }         
        }
        /// <summary>
        /// Show login page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult SignIn()
        {
            return View();
        }

        /// <summary>
        /// Login user
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SignIn(User user)
        {
            return View();
        }
    }
}
