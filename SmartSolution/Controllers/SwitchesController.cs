﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using SmartSolutionAPILib;
using WebPortal.Models.Switches;
using WebPortal.Services.Core.Switches;

namespace WebPortal.Controllers
{
    public class SwitchesController : Controller
    {
        protected static ISwitchesService SwitchesService { get; private set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public SwitchesController(ISwitchesService switchesService)
        {
            SwitchesService = switchesService;
        }

        /// <summary>
        /// Index page
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            try
            {
                IRestResponse result = SmartSolutionAPI.Get(SwitchesService.Configuration.APIBaseAddress, SwitchesService.Configuration.Switches, "");
                if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    TempData["Error"] = result.Content;
                    return RedirectToAction("Error", "Home", null);
                }
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
                IRestResponse result = SmartSolutionAPI.Get(SwitchesService.Configuration.APIBaseAddress, SwitchesService.Configuration.Switches, "");
                if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    TempData["Error"] = result.Content;
                    return RedirectToAction("Error", "Home", null);
                }
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
                IRestResponse result = SmartSolutionAPI.Get(SwitchesService.Configuration.APIBaseAddress, SwitchesService.Configuration.Switches, id);
                if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    TempData["Error"] = result.Content;
                    return RedirectToAction("Error", "Home", null);
                }
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
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                IRestResponse result = SmartSolutionAPI.Post(SwitchesService.Configuration.APIBaseAddress, SwitchesService.Configuration.Switches, switchObject.Id, switchObject);
                if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    TempData["Error"] = result.Content;
                    return RedirectToAction("Error", "Home", null);
                }
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
                IRestResponse result = SmartSolutionAPI.Get(SwitchesService.Configuration.APIBaseAddress, SwitchesService.Configuration.Switches, id);
                if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    TempData["Error"] = result.Content;
                    return RedirectToAction("Error", "Home", null);
                }
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
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                IRestResponse result = SmartSolutionAPI.Put(SwitchesService.Configuration.APIBaseAddress, SwitchesService.Configuration.Switches, switchObject.Id, switchObject);
                if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    TempData["Error"] = result.Content;
                    return RedirectToAction("Error", "Home", null);
                }
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
                IRestResponse result = SmartSolutionAPI.Get(SwitchesService.Configuration.APIBaseAddress, SwitchesService.Configuration.Switches, id);
                if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    TempData["Error"] = result.Content;
                    return RedirectToAction("Error", "Home", null);
                }
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
                IRestResponse result = SmartSolutionAPI.Delete(SwitchesService.Configuration.APIBaseAddress, SwitchesService.Configuration.Switches, switchObject.Id, switchObject);
                if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    TempData["Error"] = result.Content;
                    return RedirectToAction("Error", "Home", null);
                }
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
        /// Turn on shwitch async
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult SwitchesTurnON(string id)
        {
            try
            {
                IRestResponse result = SmartSolutionAPI.Get(SwitchesService.Configuration.APIBaseAddress, SwitchesService.Configuration.Switches + "/TurnON", id);
                if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    TempData["Error"] = result.Content;
                    return RedirectToAction("Error", "Home", null);
                }
                return Ok(result.Content);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Error", "Home", null);
            }
        }

        /// <summary>
        /// Turn off shwitch async
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult SwitchesTurnOFF(string id)
        {
            try
            {
                IRestResponse result = SmartSolutionAPI.Get(SwitchesService.Configuration.APIBaseAddress, SwitchesService.Configuration.Switches + "/TurnOFF", id);
                if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    TempData["Error"] = result.Content;
                    return RedirectToAction("Error", "Home", null);
                }
                return Ok(result.Content);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Error", "Home", null);
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