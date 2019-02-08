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
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<ServerData>> Get()
        {
            return Ok(_serverManager.GetServers());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<ServerData> Get(int id)
        {
            var server = _serverManager.GetServers().FirstOrDefault(s => s.ServerKey == id);
            if (server != null)
                return Ok(server);
            else
                return NotFound();
        }
    }
}
