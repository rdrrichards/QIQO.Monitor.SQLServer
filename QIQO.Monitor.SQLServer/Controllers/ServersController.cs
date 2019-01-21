using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using QIQO.Monitor.SQLServer.Data;

namespace QIQO.Monitor.SQLServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServersController : ControllerBase
    {
        private readonly IServerRepository _serverRepository;

        public ServersController(IServerRepository serverRepository)
        {
            _serverRepository = serverRepository;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<ServerData>> Get()
        {
            return Ok(_serverRepository.GetAll());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<ServerData> Get(int id)
        {
            var server = _serverRepository.GetByID(id);
            if (server.ServerKey != 0)
                return Ok(_serverRepository.GetByID(id));
            else
                return NotFound();
        }
    }
}
