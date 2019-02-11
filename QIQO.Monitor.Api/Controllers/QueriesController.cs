using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using QIQO.Monitor.SQLServer.Data;

namespace QIQO.Monitor.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueriesController : ControllerBase
    {
        private readonly ICoreCacheService _cacheService;

        public QueriesController(ICoreCacheService cacheService)
        {
            _cacheService = cacheService;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<QueryData>> Get()
        {
            return Ok(_cacheService.GetQueries());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<QueryData> Get(int id)
        {
            var query = _cacheService.GetQuery(id);
            if (query.QueryKey != 0)
                return Ok(query);
            else
                return NotFound();
        }

        // GET api/values/5
        [HttpGet("{name}")]
        public ActionResult<QueryData> Get(string name)
        {
            var query = _cacheService.GetQuery(name);
            if (query.QueryKey != 0)
                return Ok(query);
            else
                return NotFound();
        }
    }
}
