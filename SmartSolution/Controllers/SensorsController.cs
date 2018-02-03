using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SmartSolutionAPILib;
using System;
using System.Collections.Generic;
using WebPortal.Models.Sensors;
using WebPortal.Services;
using RestSharp;

namespace WebPortal.Controllers
{
    public class SensorsController : Controller
    {
        protected static SensorsService SensorsService { get; private set; }

        public SensorsController(SensorsService sensorsService)
        {
            SensorsService = sensorsService;

            if (SensorsService == null)
            {
                SensorsService = new SensorsService();
            }
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                IRestResponse result = SmartSolutionAPI.Get(Configuration.APIBaseAddress, Configuration.Sensors, "");
                IEnumerable<ISensor> sensors = JsonConvert.DeserializeObject<IEnumerable<Sensor>>(result.Content);
                return View(sensors);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Error", "Home", null);
            }
        }

        [HttpGet]
        public IActionResult SensorsConfiguration()
        {
            try
            {
                IRestResponse result = SmartSolutionAPI.Get(Configuration.APIBaseAddress, Configuration.Sensors, "");
                IEnumerable<ISensor> sensors = JsonConvert.DeserializeObject<IEnumerable<Sensor>>(result.Content);
                return View(sensors);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Error", "Home", null);
            }
        }

        [HttpGet]
        public IActionResult SensorsDetails(string id)
        {
            try
            {
                IRestResponse result = SmartSolutionAPI.Get(Configuration.APIBaseAddress, Configuration.Sensors, id);
                ISensor sensorObj = JsonConvert.DeserializeObject<Sensor>(result.Content);
                return View(sensorObj);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Error", "Home", null);
            }
        }

        [HttpGet]
        public IActionResult SensorsCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SensorsCreate(Sensor sensorObject)
        {
            try
            {
                IRestResponse result = SmartSolutionAPI.Post(Configuration.APIBaseAddress, Configuration.Sensors, sensorObject.Id, sensorObject);
                return RedirectToAction("SensorsConfiguration");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Error", "Home", null);
            }
        }

        [HttpGet]
        public IActionResult SensorsEdit(string id)
        {
            try
            {
                IRestResponse result = SmartSolutionAPI.Get(Configuration.APIBaseAddress, Configuration.Sensors, id);
                ISensor sensorObj = JsonConvert.DeserializeObject<Sensor>(result.Content);
                return View(sensorObj);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Error", "Home", null);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SensorsEdit(Sensor sensorObject)
        {
            try
            {
                IRestResponse result = SmartSolutionAPI.Put(Configuration.APIBaseAddress, Configuration.Sensors, sensorObject.Id, sensorObject);
                return RedirectToAction("SensorsConfiguration");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Error", "Home", null);
            }
        }

        [HttpGet]
        public IActionResult SensorsDelete(string id)
        {
            try
            {
                IRestResponse result = SmartSolutionAPI.Get(Configuration.APIBaseAddress, Configuration.Sensors, id);
                ISensor sensorObj = JsonConvert.DeserializeObject<Sensor>(result.Content);
                return View(sensorObj);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Error", "Home", null);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SensorsDelete(Sensor sensorObject)
        {
            try
            {
                IRestResponse result = SmartSolutionAPI.Delete(Configuration.APIBaseAddress, Configuration.Sensors, sensorObject.Id, sensorObject);
                ISensor switchObj = JsonConvert.DeserializeObject<Sensor>(result.Content);
                return RedirectToAction("SensorsConfiguration");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Error", "Home", null);
            }
        }

        [HttpGet]
        public IActionResult SensorsValues()
        {
            try
            {
                IRestResponse result = SmartSolutionAPI.Get(Configuration.APIBaseAddress, Configuration.Sensors, "");
                return Ok(result.Content);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Error", "Home", null);
            }
        }

        [HttpGet]
        public IActionResult SensorsGenerator()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SensorsGenerator(int numOfSens)
        {
            try
            {
                SensorsService.GenerateTestSensors(numOfSens);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Error");
            }


        }

        [HttpGet]
        public IActionResult DeleteMockupSensors()
        {
            return View();
        }

        [HttpGet]
        public IActionResult DeleteAllMockupSensors()
        {
            try
            {
                SensorsService.DeleteMockupSensors();
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