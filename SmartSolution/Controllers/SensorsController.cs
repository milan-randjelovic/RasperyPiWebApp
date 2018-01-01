using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using WebPortal.Models;
using WebPortal.Models.Sensors;

namespace WebPortal.Controllers
{
    public class SensorsController : Controller
    {
        IMongoCollection<Sensor> mongoCollection;

        public SensorsController()
        {
            MongoClient client = new MongoClient("mongodb://SmartSolution:SmartSolution2017@35.160.134.78:19735/SmartSolution");
            IMongoDatabase dbContext = client.GetDatabase("SmartSolution");
            this.mongoCollection = dbContext.GetCollection<Sensor>("Sensors");
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<ISensor> sensors;

            try
            {
                sensors = this.mongoCollection.Find(s => s.Id != "").ToList();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return Redirect("Error");
            }

            return View(sensors);
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            ISensor sensor;

            try
            {
                sensor = mongoCollection.Find(sens => sens.Id == id).SingleOrDefault();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return Redirect("Error");
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
                this.mongoCollection.InsertOne(sensor);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return Redirect("Error");
            }

            return RedirectToAction("Index","Sensors");
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            ISensor sensor;

            try
            {
                sensor = mongoCollection.Find(sens => sens.Id == id).SingleOrDefault();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return Redirect("Error");
            }

            return View(sensor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Sensor sensor)
        {
            try
            {
                this.mongoCollection.FindOneAndReplace((sens => sens.Id == sensor.Id), sensor);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return Redirect("Error");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            ISensor sensor;

            try
            {
                sensor = this.mongoCollection.Find(s => s.Id == id).SingleOrDefault();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return Redirect("Error");
            }

            return View(sensor);
        }

        [HttpPost]
        public IActionResult DeleteSensor(string id)
        {
            try
            {
                this.mongoCollection.DeleteOne(s => s.Id == id);

            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return Redirect("Error");
            }

            return RedirectToAction("Index");
        }
    }
}