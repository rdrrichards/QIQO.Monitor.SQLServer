using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using QIQO.Monitor.SQLServer.Data;

namespace QIQO.Monitor.SQLServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServersController : ControllerBase
    {
        private readonly ICoreCacheService _cacheService;

        public ServersController(ICoreCacheService cacheService)
        {
            _cacheService = cacheService;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<ServerData>> Get()
        {
            return Ok(_cacheService.GetServers());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<ServerData> Get(int id)
        {
            var server = _cacheService.GetServer(id);
            if (server.ServerKey != 0)
                return Ok(server);
            else
                return NotFound();
        }
    }
}
