using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using QIQO.Monitor.Api.Services;
using QIQO.Monitor.SQLServer.Data;

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
        [HttpGet]
        public ActionResult<IEnumerable<Server>> Get()
        {
            return Ok(_serverManager.GetServers());
        }


        /// <summary>
        /// Get a Server being managed
        /// </summary>
        /// <returns>200 - Ok</returns>
        [HttpGet("{id}")]
        public ActionResult<Server> Get(int id)
        {
            var server = _serverManager.GetServers().FirstOrDefault(s => s.ServerKey == id);
            if (server != null)
                return Ok(server);
            else
                return NotFound();
        }


        /// <summary>
        /// Add a new Server to be managed
        /// </summary>
        /// <returns>200 - Ok</returns>
        [HttpPost()]
        public ActionResult<Server> Post([FromBody] ServerAdd server)
        {
            if (server == null) return BadRequest("Invalid server parameter");

            var newEnv = _serverManager.AddServer(server);
            if (newEnv != null)
                return Created("", newEnv);
            else
                return BadRequest();
        }

        /// <summary>
        /// Update an existing Server being managed
        /// </summary>
        /// <returns>200 - Ok</returns>
        [HttpPut("{id}")]
        public ActionResult<Server> Put(int id, [FromBody] ServerUpdate server)
        {
            if (server == null) return BadRequest("Invalid server parameter");

            var newEnv = _serverManager.UpdateServer(id, server);
            if (newEnv != null)
                return Created("", newEnv);
            else
                return BadRequest();
        }

        /// <summary>
        /// Delete an existing Server being managed
        /// </summary>
        /// <returns>200 - Ok</returns>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (id == 0) return BadRequest("Invalid id parameter");

            _serverManager.DeleteServer(id);
            return NoContent();
        }
    }
}
