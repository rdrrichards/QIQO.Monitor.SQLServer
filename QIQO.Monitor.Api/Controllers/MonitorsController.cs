using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using QIQO.Monitor.Api.Services;
using QIQO.Monitor.SQLServer.Data;

namespace QIQO.Monitor.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonitorsController : ControllerBase
    {
        private readonly IMonitorManager _serviceManager;

        public MonitorsController(IMonitorManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        /// <summary>
        /// Get a collection of all Monitors being managed
        /// </summary>
        /// <returns>200 - Ok</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Monitor>> Get()
        {
            return Ok(_serviceManager.GetMonitors());
        }


        /// <summary>
        /// Get a Monitor being managed
        /// </summary>
        /// <returns>200 - Ok</returns>
        [HttpGet("{id}")]
        public ActionResult<Monitor> Get(int id)
        {
            var service = _serviceManager.GetMonitors().FirstOrDefault(s => s.MonitorKey == id);
            if (service != null)
                return Ok(service);
            else
                return NotFound();
        }


        /// <summary>
        /// Add a new Monitor to be managed
        /// </summary>
        /// <returns>200 - Ok</returns>
        [HttpPost()]
        public ActionResult<Monitor> Post([FromBody] MonitorAdd service)
        {
            if (service == null) return BadRequest("Invalid server parameter");

            var newEnv = _serviceManager.AddMonitor(service);
            if (newEnv != null)
                return Created("", newEnv);
            else
                return BadRequest();
        }

        /// <summary>
        /// Update an existing Monitor being managed
        /// </summary>
        /// <returns>200 - Ok</returns>
        [HttpPut("{id}")]
        public ActionResult<Monitor> Put(int id, [FromBody] MonitorUpdate service)
        {
            if (service == null) return BadRequest("Invalid service parameter");

            var newEnv = _serviceManager.UpdateMonitor(id, service);
            if (newEnv != null)
                return Created("", newEnv);
            else
                return BadRequest();
        }

        /// <summary>
        /// Delete an existing Monitor being managed
        /// </summary>
        /// <returns>200 - Ok</returns>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (id == 0) return BadRequest("Invalid id parameter");

            _serviceManager.DeleteMonitor(id);
            return NoContent();
        }
    }
}
