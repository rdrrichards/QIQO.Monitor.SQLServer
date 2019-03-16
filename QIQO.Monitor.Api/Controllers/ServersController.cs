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
        public ActionResult<ServerData> Get(int id)
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
            // var server = _serverManager.GetServers().FirstOrDefault(s => s.ServerKey == id);
            if (server != null)
                return Ok(server);
            else
                return NotFound();
        }

        /// <summary>
        /// Update an existing Server being managed
        /// </summary>
        /// <returns>200 - Ok</returns>
        [HttpPut("{id}")]
        public ActionResult<Server> Put(int id, [FromBody] ServerUpdate server)
        {
            // var server = _serverManager.GetServers().FirstOrDefault(s => s.ServerKey == id);
            if (server != null)
                return Ok(server);
            else
                return NotFound();
        }
    }
}
