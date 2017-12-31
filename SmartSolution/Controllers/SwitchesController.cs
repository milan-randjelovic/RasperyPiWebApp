using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using WebPortal.Models;
using WebPortal.Models.Switches;
using WebPortal.Services;

namespace WebPortal.Controllers
{
    public class SwitchesController : Controller
    {
        private IMongoCollection<Switch> mongoCollection;
        private static SwitchesService SwitchesService;

        public SwitchesController()
        {
            MongoClient client = new MongoClient("mongodb://SmartSolution:SmartSolution2017@35.160.134.78:19735/SmartSolution");
            IMongoDatabase dbContext = client.GetDatabase("SmartSolution");
            this.mongoCollection = dbContext.GetCollection<Switch>("Switches");

            if (SwitchesService == null)
            {
                SwitchesService = new SwitchesService();
            }
        }

        // GET: Switches
        public IActionResult Index()
        {
            IEnumerable<ISwitch> switches;

            try
            {
                switches = this.mongoCollection.Find(s => s.Id != "").ToList();
                SwitchesService.Switches = switches;
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return Redirect("Error");
            }

            return View(switches);
        }

        // GET: Switches/Details/5
        public IActionResult Details(string id)
        {
            ISwitch switchObj;

            try
            {
                switchObj = mongoCollection.Find(sw => sw.Id == id).SingleOrDefault();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return Redirect("Error");
            }

            return View(switchObj);
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
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return Redirect("Error");
            }

            return RedirectToAction("Index");
        }

        // GET: Switches/Edit/5
        public IActionResult Edit(string id)
        {
            ISwitch switchObj;
            try
            {
                switchObj = mongoCollection.Find(sw => sw.Id == id).SingleOrDefault();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return Redirect("Error");
            }

            return View(switchObj);
        }

        // POST: Switches/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Switch switchObj)
        {
            try
            {
                mongoCollection.FindOneAndReplace(sw => sw.Id == switchObj.Id, switchObj);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return Redirect("Error");
            }

            return RedirectToAction("Index");
        }

        // GET: Switches/Delete/5
        public IActionResult Delete(string id)
        {
            ISwitch switchObj;

            try
            {
                switchObj = mongoCollection.Find(sw => sw.Id == id).SingleOrDefault();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return Redirect("Error");
            }

            return View(switchObj);
        }

        // POST: Switches/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteSwitch(string id)
        {
            try
            {
                mongoCollection.FindOneAndDelete(sw => sw.Id == id);
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