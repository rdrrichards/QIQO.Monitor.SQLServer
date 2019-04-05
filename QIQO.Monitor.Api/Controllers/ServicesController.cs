using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using QIQO.Monitor.Api.Services;
using QIQO.Monitor.SQLServer.Data;

namespace QIQO.Monitor.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public ServicesController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        /// <summary>
        /// Get a collection of all Services being managed
        /// </summary>
        /// <returns>200 - Ok</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Service>> Get()
        {
            return Ok(_serviceManager.GetServices());
        }


        /// <summary>
        /// Get a Service being managed
        /// </summary>
        /// <returns>200 - Ok</returns>
        [HttpGet("{id}")]
        public ActionResult<Service> Get(int id)
        {
            var service = _serviceManager.GetServices().FirstOrDefault(s => s.ServiceKey == id);
            if (service != null)
                return Ok(service);
            else
                return NotFound();
        }


        /// <summary>
        /// Add a new Service to be managed
        /// </summary>
        /// <returns>200 - Ok</returns>
        [HttpPost()]
        public ActionResult<Service> Post([FromBody] ServiceAdd service)
        {
            if (service == null) return BadRequest("Invalid server parameter");

            var newEnv = _serviceManager.AddService(service);
            if (newEnv != null)
                return Created("", newEnv);
            else
                return BadRequest();
        }

        /// <summary>
        /// Update an existing Service being managed
        /// </summary>
        /// <returns>200 - Ok</returns>
        [HttpPut("{id}")]
        public ActionResult<Service> Put(int id, [FromBody] ServiceUpdate service)
        {
            if (service == null) return BadRequest("Invalid service parameter");

            var newEnv = _serviceManager.UpdateService(id, service);
            if (newEnv != null)
                return Created("", newEnv);
            else
                return BadRequest();
        }

        /// <summary>
        /// Delete an existing Service being managed
        /// </summary>
        /// <returns>200 - Ok</returns>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (id == 0) return BadRequest("Invalid id parameter");

            _serviceManager.DeleteService(id);
            return NoContent();
        }
    }
}
