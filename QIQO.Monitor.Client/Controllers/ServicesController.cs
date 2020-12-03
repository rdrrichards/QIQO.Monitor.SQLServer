using Microsoft.AspNetCore.Mvc;
using QIQO.Monitor.Client;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QIQO.Monitor.Client
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
        /// <returns>500 - Internal Error</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Service>> Get()
        {
            try
            {
                return Ok(_serviceManager.GetServices());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        /// <summary>
        /// Get a Service being managed
        /// </summary>
        /// <returns>200 - Ok</returns>
        /// <returns>404 - Not Found</returns>
        /// <returns>500 - Internal Error</returns>
        [HttpGet("{id}")]
        public ActionResult<Service> Get(int id)
        {
            try
            {
                var service = _serviceManager.GetServices().FirstOrDefault(s => s.ServiceKey == id);
                if (service != null)
                    return Ok(service);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        /// <summary>
        /// Add a new Service to be managed
        /// </summary>
        /// <returns>201 - Created</returns>
        /// <returns>400 - Bad Request</returns>
        /// <returns>500 - Internal Error</returns>
        [HttpPost()]
        public ActionResult<Service> Post([FromBody] ServiceAdd service)
        {
            if (service == null) return BadRequest("Invalid server parameter");
            try
            {
                var newEnv = _serviceManager.AddService(service);
                if (newEnv != null)
                    return Created("", newEnv);
                else
                    return StatusCode(500, service);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Update an existing Service being managed
        /// </summary>
        /// <returns>202 - Accepted</returns>
        /// <returns>400 - Bad Request</returns>
        /// <returns>500 - Internal Error</returns>
        [HttpPut("{id}")]
        public ActionResult<Service> Put(int id, [FromBody] ServiceUpdate service)
        {
            if (service == null) return BadRequest("Invalid service parameter");

            try
            {
                var newEnv = _serviceManager.UpdateService(id, service);
                if (newEnv != null)
                    return Accepted(newEnv);
                else
                    return StatusCode(500, service);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Delete an existing Service being managed
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
                _serviceManager.DeleteService(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
