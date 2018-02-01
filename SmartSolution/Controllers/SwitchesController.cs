using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using SmartSolutionAPILib;
using WebPortal.Models.Switches;
using WebPortal.Services;

namespace WebPortal.Controllers
{
    public class SwitchesController : Controller
    {
        protected static SwitchesService SwitchesService { get; private set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public SwitchesController(SwitchesService switchesService)
        {
            SwitchesService = switchesService;

            if (SwitchesService == null)
            {
                SwitchesService = new SwitchesService();
            }
        }

        /// <summary>
        /// Index page
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            try
            {
                IRestResponse result = SmartSolutionAPI.Get(Request.Host.Value, Configuration.Switches, "");
                IEnumerable<ISwitch> switches = JsonConvert.DeserializeObject<IEnumerable<Switch>>(result.Content);
                return View(switches);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Error", "Home", null);
            }
        }

        /// <summary>
        /// Index page
        /// </summary>
        /// <returns></returns>
        public IActionResult SwitchesConfiguration()
        {
            try
            {
                IRestResponse result = SmartSolutionAPI.Get(Request.Host.Value, Configuration.Switches, "");
                IEnumerable<ISwitch> switches = JsonConvert.DeserializeObject<IEnumerable<Switch>>(result.Content);
                return View(switches);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Error", "Home", null);
            }
        }

        /// <summary>
        /// Details page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult SwitchesDetails(string id)
        {
            try
            {
                IRestResponse result = SmartSolutionAPI.Get(Request.Host.Value, Configuration.Switches, id);
                ISwitch switchObj = JsonConvert.DeserializeObject<Switch>(result.Content);
                return View(switchObj);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Error", "Home", null);
            }
        }

        /// <summary>
        /// Create new page
        /// </summary>
        /// <returns></returns>
        public IActionResult SwitchesCreate()
        {
            return View();
        }

        /// <summary>
        /// Create switch 
        /// </summary>
        /// <param name="switchObject"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SwitchesCreate(Switch switchObject)
        {
            try
            {
                IRestResponse result = SmartSolutionAPI.Post(Request.Host.Value, WebPortal.Configuration.Switches, switchObject.Id, switchObject);
                return RedirectToAction("SwitchesConfiguration");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Error", "Home", null);
            }
        }

        /// <summary>
        /// Edit page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult SwitchesEdit(string id)
        {
            try
            {
                IRestResponse result = SmartSolutionAPI.Get(Request.Host.Value, Configuration.Switches, id);
                ISwitch switchObj = JsonConvert.DeserializeObject<Switch>(result.Content);
                return View(switchObj);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Error", "Home", null);
            }
        }

        /// <summary>
        /// Edit switch
        /// </summary>
        /// <param name="switchObj"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SwitchesEdit(Switch switchObject)
        {
            try
            {
                IRestResponse result = SmartSolutionAPI.Put(Request.Host.Value, WebPortal.Configuration.Switches, switchObject.Id, switchObject);
                return RedirectToAction("SwitchesConfiguration");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Error", "Home", null);
            }
        }

        /// <summary>
        /// Delete page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult SwitchesDelete(string id)
        {
            try
            {
                IRestResponse result = SmartSolutionAPI.Get(Request.Host.Value, Configuration.Switches, id);
                ISwitch switchObj = JsonConvert.DeserializeObject<Switch>(result.Content);
                return View(switchObj);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Error", "Home", null);
            }
        }

        /// <summary>
        /// Delete switch
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SwitchesDelete(Switch switchObject)
        {
            try
            {
                IRestResponse result = SmartSolutionAPI.Delete(Request.Host.Value, Configuration.Switches, switchObject.Id, switchObject);
                ISwitch switchObj = JsonConvert.DeserializeObject<Switch>(result.Content);
                return RedirectToAction("SwitchesConfiguration");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Error", "Home", null);
            }
        }

        /// <summary>
        /// Turn on switch
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult SwitchesTurnON(string id)
        {
            try
            {
                SwitchesService.TurnON(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Error", "Home", null);
            }
        }

        /// <summary>
        /// Turn off switch
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult SwitchesTurnOFF(string id)
        {
            try
            {
                SwitchesService.TurnOFF(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Error", "Home", null);
            }
        }

        /// <summary>
        /// Turn on shwitch async
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult SwitchesTurnONAsync(string id)
        {
            try
            {
                SwitchesService.TurnON(id);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.ServiceUnavailable, ex.Message);
            }
        }

        /// <summary>
        /// Turn off shwitch async
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult SwitchesTurnoFFAsync(string id)
        {
            try
            {
                SwitchesService.TurnOFF(id);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.ServiceUnavailable, ex.Message);
            }
        }

        [HttpGet]
        public IActionResult SwitchesGenerator()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SwitchesGenerator(int numOfSwitches)
        {
            try
            {
                SwitchesService.GenerateTestSwitches(numOfSwitches);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        public IActionResult DeleteMockupSwitches()
        {
            return View();
        }

        [HttpGet]
        public IActionResult DeleteAllMockupSwitches()
        {
            try
            {
                SwitchesService.DeleteMockupSwitches();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Error");
            }
        }
    }
}