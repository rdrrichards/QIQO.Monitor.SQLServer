using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using QIQO.Monitor.Api.Services;
using QIQO.Monitor.SQLServer.Data;

namespace QIQO.Monitor.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlockingController : QIQOControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public BlockingController(IDbContextFactory dbContextFactory, 
            IDataRepositoryFactory repositoryFactory, IServiceManager serviceManager) : base(dbContextFactory, repositoryFactory)
        {
            _serviceManager = serviceManager;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<BlockingData>> Get(int id)
        {
            var server = _serviceManager.GetServices().FirstOrDefault(s => s.ServiceKey == id);
            if (server != null)
            {
                var monitor = server.Monitors.FirstOrDefault(m => m.MonitorType == MonitorType.SqlServer &&
                    m.MonitorCategory == MonitorCategory.DetectBlocking);
                var query = monitor.Queries.FirstOrDefault();
                CreateContext(server.ServiceSource);
                var repo = _repositoryFactory.GetDataRepository<IBlockingRepository>();
                var blockingData = repo.Get(query.QueryText);
                return Ok(blockingData);
            }
            return NotFound();
        }
    }
}
