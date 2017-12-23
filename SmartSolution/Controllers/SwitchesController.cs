using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using WebPortal.Models;

namespace WebPortal.Controllers
{
    public class SwitchesController : Controller
    {
        IMongoCollection<Switch> mongoCollection;

        public SwitchesController()
        {
            MongoClient client = new MongoClient("mongodb://SmartSolution:SmartSolution2017@35.160.134.78:19735/SmartSolution");
            IMongoDatabase dbContext = client.GetDatabase("SmartSolution");
            this.mongoCollection = dbContext.GetCollection<Switch>("Switches");
        }

        // GET: Switches
        public IActionResult Index()
        {
            List<Switch> switches = this.mongoCollection.Find(s => s.Id != "").ToList();
            return View(switches);
        }

        // GET: Switches/Details/5
        public IActionResult Details(int id)
        {
            return View();
        }

        // GET: Switches/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Switches/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Switch switchObject)
        {
            try
            {
                this.mongoCollection.InsertOne(switchObject);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Switches/Edit/5
        public IActionResult Edit(int id)
        {
            return View();
        }

        // POST: Switches/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Switches/Delete/5
        public IActionResult Delete(int id)
        {
            return View();
        }

        // POST: Switches/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}