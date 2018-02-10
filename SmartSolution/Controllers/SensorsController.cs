using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SmartSolutionAPILib;
using System;
using System.Collections.Generic;
using WebPortal.Models.Sensors;
using RestSharp;
using WebPortal.Services.Core.Sensors;

namespace WebPortal.Controllers
{
    public class SensorsController : Controller
    {
        protected string APIBaseAddress { get; set; }
        protected static ISensorsService SensorsService { get; private set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="sensorsService"></param>
        public SensorsController(ISensorsService sensorsService)
        {
            SensorsService = sensorsService;
            this.APIBaseAddress = sensorsService.Configuration.APIBaseAddress;
        }

        /// <summary>
        /// Index page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                IRestResponse result = SmartSolutionAPI.Get(SensorsService.Configuration.APIBaseAddress, SensorsService.Configuration.Sensors, "");
                if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    TempData["Error"] = result.Content;
                    return RedirectToAction("Error", "Home", null);
                }
                IEnumerable<ISensor> sensors = JsonConvert.DeserializeObject<IEnumerable<Sensor>>(result.Content);
                return View(sensors);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Error", "Home", null);
            }
        }

        /// <summary>
        /// Sensor configuration page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult SensorsConfiguration()
        {
            try
            {
                IRestResponse result = SmartSolutionAPI.Get(SensorsService.Configuration.APIBaseAddress, SensorsService.Configuration.Sensors, "");
                if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    TempData["Error"] = result.Content;
                    return RedirectToAction("Error", "Home", null);
                }
                IEnumerable<ISensor> sensors = JsonConvert.DeserializeObject<IEnumerable<Sensor>>(result.Content);
                return View(sensors);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Error", "Home", null);
            }
        }

        /// <summary>
        /// Sensor details page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult SensorsDetails(string id)
        {
            try
            {
                IRestResponse result = SmartSolutionAPI.Get(SensorsService.Configuration.APIBaseAddress, SensorsService.Configuration.Sensors, id);
                if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    TempData["Error"] = result.Content;
                    return RedirectToAction("Error", "Home", null);
                }
                ISensor sensorObj = JsonConvert.DeserializeObject<Sensor>(result.Content);
                return View(sensorObj);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Error", "Home", null);
            }
        }

        /// <summary>
        /// Sensor create page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult SensorsCreate()
        {
            return View();
        }

        /// <summary>
        /// Create sensor
        /// </summary>
        /// <param name="sensorObject"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SensorsCreate(Sensor sensorObject)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                IRestResponse result = SmartSolutionAPI.Post(SensorsService.Configuration.APIBaseAddress, SensorsService.Configuration.Sensors, sensorObject.Id, sensorObject);
                if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    TempData["Error"] = result.Content;
                    return RedirectToAction("Error", "Home", null);
                }
                return RedirectToAction("SensorsConfiguration");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Error", "Home", null);
            }
        }

        /// <summary>
        /// Sensor edit page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult SensorsEdit(string id)
        {
            try
            {
                IRestResponse result = SmartSolutionAPI.Get(SensorsService.Configuration.APIBaseAddress, SensorsService.Configuration.Sensors, id);
                if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    TempData["Error"] = result.Content;
                    return RedirectToAction("Error", "Home", null);
                }
                ISensor sensorObj = JsonConvert.DeserializeObject<Sensor>(result.Content);
                return View(sensorObj);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Error", "Home", null);
            }
        }

        /// <summary>
        /// Edit sensor
        /// </summary>
        /// <param name="sensorObject"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SensorsEdit(Sensor sensorObject)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                IRestResponse result = SmartSolutionAPI.Put(SensorsService.Configuration.APIBaseAddress, SensorsService.Configuration.Sensors, sensorObject.Id, sensorObject);
                if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    TempData["Error"] = result.Content;
                    return RedirectToAction("Error", "Home", null);
                }
                return RedirectToAction("SensorsConfiguration");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Error", "Home", null);
            }
        }

        /// <summary>
        /// Sensor delete page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult SensorsDelete(string id)
        {
            try
            {
                IRestResponse result = SmartSolutionAPI.Get(SensorsService.Configuration.APIBaseAddress, SensorsService.Configuration.Sensors, id);
                if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    TempData["Error"] = result.Content;
                    return RedirectToAction("Error", "Home", null);
                }
                ISensor sensorObj = JsonConvert.DeserializeObject<Sensor>(result.Content);
                return View(sensorObj);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Error", "Home", null);
            }
        }

        /// <summary>
        /// Delete sensor
        /// </summary>
        /// <param name="sensorObject"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SensorsDelete(Sensor sensorObject)
        {
            try
            {
                IRestResponse result = SmartSolutionAPI.Delete(SensorsService.Configuration.APIBaseAddress, SensorsService.Configuration.Sensors, sensorObject.Id, sensorObject);
                if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    TempData["Error"] = result.Content;
                    return RedirectToAction("Error", "Home", null);
                }
                ISensor switchObj = JsonConvert.DeserializeObject<Sensor>(result.Content);
                return RedirectToAction("SensorsConfiguration");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Error", "Home", null);
            }
        }

        /// <summary>
        /// Refresh sensor values
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult SensorsValues()
        {
            try
            {
                IRestResponse result = SmartSolutionAPI.Get(SensorsService.Configuration.APIBaseAddress, SensorsService.Configuration.Sensors, "");
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
        /// Get sensor log
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Log(DateTime from, DateTime to)
        {
            try
            {
                IRestResponse result = SmartSolutionAPI.Get(SensorsService.Configuration.APIBaseAddress, SensorsService.Configuration.Sensors + "/Log", from.ToShortDateString() + "&" + to.ToShortDateString());
                if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    TempData["Error"] = result.Content;
                    return RedirectToAction("Error", "Home", null);
                }
                IEnumerable<ISensorLog> sensors = JsonConvert.DeserializeObject<IEnumerable<SensorLog>>(result.Content);
                return View(sensors);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Error", "Home", null);
            }
        }
    }
}