using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using QIQO.Monitor.Api.Services;

namespace QIQO.Monitor.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonitorsController : ControllerBase
    {
        private readonly IMonitorManager _monitorManager;

        public MonitorsController(IMonitorManager monitorManager)
        {
            _monitorManager = monitorManager;
        }

        /// <summary>
        /// Get a collection of all Monitors being managed
        /// </summary>
        /// <returns>200 - Ok</returns>
        /// <returns>500 - Internal Error</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Monitor>> Get()
        {
            try
            {
                return Ok(_monitorManager.GetMonitors());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        /// <summary>
        /// Get a Monitor being managed
        /// </summary>
        /// <returns>200 - Ok</returns>
        /// <returns>404 - Not Found</returns>
        /// <returns>500 - Internal Error</returns>
        [HttpGet("{id}")]
        public ActionResult<Monitor> Get(int id)
        {
            try
            {
                var monitor = _monitorManager.GetMonitors().FirstOrDefault(s => s.MonitorKey == id);
                if (monitor != null)
                    return Ok(monitor);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        /// <summary>
        /// Add a new Monitor to be managed
        /// </summary>
        /// <returns>201 - Created</returns>
        /// <returns>400 - Bad Request</returns>
        /// <returns>500 - Internal Error</returns>
        [HttpPost()]
        public ActionResult<Monitor> Post([FromBody] MonitorAdd monitor)
        {
            if (monitor == null) return BadRequest("Invalid server parameter");

            try
            {
                var newEnv = _monitorManager.AddMonitor(monitor);
                if (newEnv != null)
                    return Created("", newEnv);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Update an existing Monitor being managed
        /// </summary>
        /// <returns>202 - Accepted</returns>
        /// <returns>400 - Bad Request</returns>
        /// <returns>500 - Internal Error</returns>
        [HttpPut("{id}")]
        public ActionResult<Monitor> Put(int id, [FromBody] MonitorUpdate monitor)
        {
            if (monitor == null) return BadRequest("Invalid monitor parameter");

            try
            {
                var newEnv = _monitorManager.UpdateMonitor(id, monitor);
                if (newEnv != null)
                    return Created("", newEnv);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Delete an existing Monitor being managed
        /// </summary>
        /// <returns>204 - No Content</returns>
        /// <returns>400 - Bad Request</returns>
        /// <returns>500 - Internal Error</returns>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (id == 0) return BadRequest("Invalid id parameter");

            try
            {
                _monitorManager.DeleteMonitor(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
