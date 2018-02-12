using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebPortal.Models.Switches;
using WebPortal.Services.Core.Switches;

namespace WebPortal.Controllers.API
{
    [Produces("application/json")]
    [Route("api/Switches")]
    [Authorize]
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
        /// Get all switches
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<ISwitch> result = SwitchesService.Switches;

            if (result == null)
            {
                return NotFound();
            }

            return new JsonResult(result);
        }

        /// <summary>
        /// Get switch by id
        /// </summary>
        /// <param name="id">Switch Id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            ISwitch result = SwitchesService.GetSwitchFromDatabase(id);

            if (result == null)
            {
                return NotFound(id);
            }

            return new JsonResult(result);
        }

        /// <summary>
        /// Get switches log
        /// </summary>
        /// <param name="id">Switch id</param>
        /// <returns></returns>
        [HttpGet("Log")]
        public IActionResult Log(DateTime from, DateTime to)
        {
            try
            {
                IEnumerable<ISwitchLog> result = SwitchesService.GetSwitchesLog(from, to);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Create switch
        /// </summary>
        /// <param name="switchObject">Switch data</param>
        [HttpPost]
        public IActionResult Post([FromBody]Switch switchObject)
        {
            try
            {
                SwitchesService.CreateNew(switchObject);
                return Created(Request.Path + switchObject.Id, switchObject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Update switch
        /// </summary>
        /// <param name="id">Switch Id</param>
        /// <param name="value">Switch data</param>
        [HttpPut]
        public IActionResult Put([FromBody]Switch switchObject)
        {
            try
            {
                SwitchesService.Update(switchObject);
                return Ok(switchObject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Detele switch
        /// </summary>
        /// <param name="id">Switch Id</param>
        [HttpDelete]
        public IActionResult Delete([FromBody]Switch switchObject)
        {
            try
            {
                SwitchesService.Delete(switchObject.Id);
                return Ok(switchObject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Turn on shwitch
        /// </summary>
        /// <param name="id">Switch id</param>
        /// <returns></returns>
        [HttpGet("TurnON/{id}")]
        public IActionResult TurnON(string id)
        {
            try
            {
                SwitchesService.TurnON(id);
                return Ok(SwitchesService.Switches.Where(sw => sw.Id == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Turn off shwitch
        /// </summary>
        /// <param name="id">Switch id</param>
        /// <returns></returns>
        [HttpGet("TurnOFF/{id}")]
        public IActionResult TurnOFF(string id)
        {
            try
            {
                SwitchesService.TurnOFF(id);
                return Ok(SwitchesService.Switches.Where(sw => sw.Id == id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
