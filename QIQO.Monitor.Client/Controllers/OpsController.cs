using Microsoft.AspNetCore.Mvc;
using QIQO.Monitor.Data;
using System;

namespace QIQO.Monitor.Client
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpsController : ControllerBase
    {
        private readonly ICoreCacheService _coreCacheService;

        public OpsController(ICoreCacheService coreCacheService)
        {
            _coreCacheService = coreCacheService;
        }

        [HttpGet("heartbeat")]
        public ActionResult<DateTime> Ping()
        {
            try
            {
                return Ok(DateTime.Now);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("refreshcache")]
        public ActionResult RefreshCache()
        {
            try
            {
                _coreCacheService.RefreshCache();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
