using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using WebPortal.Models;

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

        // GET: Sensors
        [HttpGet]
        public IActionResult Index()
        {
            List<Sensor> sensors = this.mongoCollection.Find(s => s.Id != "").ToList();
            return View(sensors);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Sensor sensor)
        {
            this.mongoCollection.InsertOne(sensor);
            return View();
        }

        [HttpPost]
        public IActionResult Edit(Sensor sensor)
        {
            this.mongoCollection.InsertOne(sensor);
            return View();
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            Sensor result = this.mongoCollection.Find(s => s.Id == id).SingleOrDefault();
            return View(result);
        }

        [HttpPost]
        public IActionResult DeleteSensor(string id)
        {
            this.mongoCollection.DeleteOne(s => s.Id == id);
            return RedirectToAction("Index");
        }
    }
}