using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebPortal.Models.Sensors;
using WebPortal.Services.Core.Sensors;

namespace WebPortal.Controllers.API
{
    [Produces("application/json")]
    [Route("api/Sensors")]
    public class SensorsController : Controller
    {
        protected static ISensorsService SensorsService { get; private set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public SensorsController(ISensorsService sensorsService)
        {
            SensorsService = sensorsService;
        }

        /// <summary>
        /// Get all sensors
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<ISensor> result = SensorsService.Sensors;

            if (result == null)
            {
                return NotFound();
            }

            return new JsonResult(result);
        }

        /// <summary>
        /// Get sensor by id
        /// </summary>
        /// <param name="id">Sensor Id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            ISensor result = SensorsService.GetSensorFromDatabase(id);

            if (result == null)
            {
                return NotFound(id);
            }

            return new JsonResult(result);
        }

        /// <summary>
        /// Create sensor
        /// </summary>
        /// <param name="switchObject">Sensor data</param>
        [HttpPost("{id}")]
        public IActionResult Post(string id, [FromBody]Sensor sensorObject)
        {
            try
            {
                SensorsService.CreateNew(sensorObject);
                return Created(Request.Path + sensorObject.Id, sensorObject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Update sensor
        /// </summary>
        /// <param name="id">Sensor Id</param>
        /// <param name="value">Sensor data</param>
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody]Sensor sensorObject)
        {
            try
            {
                SensorsService.Update(sensorObject);
                return Ok(sensorObject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Detele sensor
        /// </summary>
        /// <param name="id">Sensor Id</param>
        [HttpDelete("{id}")]
        public IActionResult Delete(string id, [FromBody]Sensor sensorObject)
        {
            try
            {
                SensorsService.Delete(id);
                return Ok(sensorObject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
