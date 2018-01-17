using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using WebPortal.Models.Sensors;
using WebPortal.Services;

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
            IEnumerable<ISensor> sensors;

            try
            {
                sensors = SensorsService.Sensors;
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Error", "Home", null);
            }

            return View(sensors);
        }

        [HttpGet]
        public IActionResult Configuration()
        {
            IEnumerable<ISensor> sensors;

            try
            {
                sensors = SensorsService.Sensors;
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Error", "Home", null);
            }

            return View(sensors);
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            ISensor sensor;

            try
            {
                sensor = SensorsService.Find(id);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Error", "Home", null);
            }

            return View(sensor);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Sensor sensor)
        {
            try
            {
                SensorsService.CreateNew(sensor);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Error", "Home", null);
            }

            return RedirectToAction("Index", "Sensors");
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            ISensor sensor;

            try
            {
                sensor = SensorsService.Find(id);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Error", "Home", null);
            }

            return View(sensor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Sensor sensor)
        {
            try
            {
                SensorsService.Update(sensor);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Error", "Home", null);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            ISensor sensor;

            try
            {
                sensor = SensorsService.Find(id);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Error", "Home", null);
            }

            return View(sensor);
        }

        [HttpPost]
        public IActionResult DeleteSensor(string id)
        {
            try
            {
                SensorsService.Delete(id);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Error", "Home", null);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult SensorsGenerator() {
            return View();
        }

        [HttpPost]
        public IActionResult SensorsGenerator(int numOfSens) {
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
        public IActionResult DeleteMockupSensors() {
            return View();
        }

        [HttpGet]
        public IActionResult DeleteAllMockupSensors() {
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