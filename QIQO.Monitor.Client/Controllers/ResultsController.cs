using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Mvc;
using System;
using QIQO.Monitor.Domain;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace QIQO.Monitor.Client
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultsController : ControllerBase
    {
        private readonly IHubContext<MonitorComms> _hubContext;

        public ResultsController(IHubContext<MonitorComms> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] object result)
        {
            try
            {
                await _hubContext.Clients.All.SendAsync("monitorresult", result);
                return Ok("");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
