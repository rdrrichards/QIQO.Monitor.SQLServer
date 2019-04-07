using System.Linq;
using Microsoft.AspNetCore.Mvc;
using QIQO.Monitor.Api.Services;
using QIQO.Monitor.SQLServer.Data;

namespace QIQO.Monitor.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VersionController : QIQOControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public VersionController(IDbContextFactory dbContextFactory, 
            IDataRepositoryFactory repositoryFactory, IServiceManager serviceManager) : base(dbContextFactory, repositoryFactory)
        {
            _serviceManager = serviceManager;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            try
            {
                var server = _serviceManager.GetServices().FirstOrDefault(s => s.ServiceKey == id);
                if (server != null)
                {
                    CreateContext(server.ServiceSource);
                    var repo = _repositoryFactory.GetDataRepository<IVersionRepository>();
                    var version = repo.Get().ToArray()[0].VersionText;
                    return Ok(version);
                }
                return NotFound();
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
