using Microsoft.AspNetCore.Mvc;
using QIQO.Monitor.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QIQO.Monitor.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServersController : ControllerBase
    {
        private readonly IServerManager _serverManager;

        public ServersController(IServerManager serverManager)
        {
            _serverManager = serverManager;
        }

        /// <summary>
        /// Get a collection of all Servers being managed
        /// </summary>
        /// <returns>200 - Ok</returns>
        /// <returns>500 - Internal Error</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Server>> Get()
        {
            try
            {
                return Ok(_serverManager.GetServers());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        /// <summary>
        /// Get a Server being managed
        /// </summary>
        /// <returns>200 - Ok</returns>
        /// <returns>404 - Not Found</returns>
        /// <returns>500 - Internal Error</returns>
        [HttpGet("{id}")]
        public ActionResult<Server> Get(int id)
        {
            try
            {
                var server = _serverManager.GetServers().FirstOrDefault(s => s.ServerKey == id);
                if (server != null)
                    return Ok(server);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        /// <summary>
        /// Add a new Server to be managed
        /// </summary>
        /// <returns>201 - Created</returns>
        /// <returns>400 - Bad Request</returns>
        /// <returns>500 - Internal Error</returns>
        [HttpPost()]
        public ActionResult<Server> Post([FromBody] ServerAdd server)
        {
            if (server == null) return BadRequest("Invalid server parameter");

            try
            {
                var newEnv = _serverManager.AddServer(server);
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
        /// Update an existing Server being managed
        /// </summary>
        /// <returns>202 - Accepted</returns>
        /// <returns>400 - Bad Request</returns>
        /// <returns>500 - Internal Error</returns>
        [HttpPut("{id}")]
        public ActionResult<Server> Put(int id, [FromBody] ServerUpdate server)
        {
            if (server == null) return BadRequest("Invalid server parameter");

            try
            {
                var newEnv = _serverManager.UpdateServer(id, server);
                if (newEnv != null)
                    return Accepted("", newEnv);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Delete an existing Server being managed
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
                _serverManager.DeleteServer(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
