﻿using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
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
            IEnumerable<ISwitch> switches;

            try
            {
                switches = SwitchesService.Switches;
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Error", "Home", null);
            }

            return View(switches);
        }

        /// <summary>
        /// Index page
        /// </summary>
        /// <returns></returns>
        public IActionResult Configuration()
        {
            IEnumerable<ISwitch> switches;

            try
            {
                switches = SwitchesService.Switches;
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Error", "Home", null);
            }

            return View(switches);
        }

        /// <summary>
        /// Details page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Details(string id)
        {
            ISwitch switchObj;

            try
            {
                switchObj = SwitchesService.GetSwitchFromDatabase(id);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Error", "Home", null);
            }

            return View(switchObj);
        }

        /// <summary>
        /// Create new page
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
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
        public IActionResult Create(Switch switchObject)
        {
            try
            {
                SwitchesService.CreateNew(switchObject);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Error", "Home", null);
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Edit page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Edit(string id)
        {
            ISwitch switchObj;
            try
            {
                switchObj = SwitchesService.GetSwitchFromDatabase(id);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Error", "Home", null);
            }

            return View(switchObj);
        }

        /// <summary>
        /// Edit switch
        /// </summary>
        /// <param name="switchObj"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Switch switchObj)
        {
            try
            {
                SwitchesService.Update(switchObj);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Error", "Home", null);
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Delete page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Delete(string id)
        {
            ISwitch switchObj;

            try
            {
                switchObj = SwitchesService.GetSwitchFromDatabase(id);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Error", "Home", null);
            }

            return View(switchObj);
        }

        /// <summary>
        /// Delete switch
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteSwitch(string id)
        {
            try
            {
                SwitchesService.Delete(id);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Error", "Home", null);
            }

            return RedirectToAction("Index");

        }

        /// <summary>
        /// Turn on switch
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult TurnON(string id)
        {
            try
            {
                SwitchesService.TurnON(id);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Error", "Home", null);
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Turn off switch
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult TurnOFF(string id)
        {
            try
            {
                SwitchesService.TurnOFF(id);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Error", "Home", null);
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Turn on shwitch async
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult TurnONAsync(string id)
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
        public IActionResult TurnoFFAsync(string id)
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
        public IActionResult SwitchesGenerator() {
            return View();
        }

        [HttpPost]
        public IActionResult SwitchesGenerator(int numOfSwitches) {
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
        public IActionResult DeleteMockupSwitches() {
            return View();
        }

        [HttpGet]
        public IActionResult DeleteAllMockupSwitches() {
            try
            {
                SwitchesService.DeleteMockupSwitches();
                return RedirectToAction("Index");
            }
            catch (Exception ex) {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Error");
            }
        }
    }
}