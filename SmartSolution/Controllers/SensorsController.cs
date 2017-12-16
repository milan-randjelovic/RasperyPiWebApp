using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Collections.Generic;
using WebPortal.Models;

namespace WebPortal.Controllers
{
    public class SensorsController : Controller
    {
        private static List<MockupSensor> sensors = new List<MockupSensor>();

        public SensorsController()
        {
            if (sensors.Count == 0)
            {
                //crealte sensor and add to list
                MockupSensor s1 = new MockupSensor();
                s1.Name = "Temperature Sensor";
                s1.Value = "32C";
                s1.Model = "TMP36";
                s1.Vendor = "CilimTech";
                s1.IsActive = true;
                sensors.Add(s1);

                //crealte sensor and add to list
                MockupSensor s2 = new MockupSensor();
                s2.Name = "Humidity Sensor";
                s2.Value = "53%";
                s2.Model = "TC134";
                s2.Vendor = "CilimTech";
                s2.IsActive = true;
                sensors.Add(s2);

                //crealte sensor and add to list
                MockupSensor s3 = new MockupSensor();
                s3.Name = "Presure Sensor";
                s3.Value = "1013mb";
                s3.Model = "P1C13";
                s3.Vendor = "CilimTech";
                s3.IsActive = false;
                sensors.Add(s3);
            }
        }

        // GET: Sensors
        [HttpGet]
        public IActionResult Index()
        {
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
            MongoClient client = new MongoClient("mongodb://SmartSolution:SmartSolution2017@35.160.134.78:19735/SmartSolution");
            IMongoDatabase dbContext = client.GetDatabase("SmartSolution");
            IMongoCollection<Sensor> mongoCollection = dbContext.GetCollection<Sensor>("Sensors");
            mongoCollection.InsertOne(sensor);

            return View();
        }
    }
}