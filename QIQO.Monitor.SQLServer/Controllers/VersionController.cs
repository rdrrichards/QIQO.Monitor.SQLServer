using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using QIQO.Monitor.SQLServer.Data;

namespace QIQO.Monitor.SQLServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VersionController : ControllerBase
    {
        private readonly IDbContextFactory _dbContextFactory;
        private readonly IDataRepositoryFactory _repositoryFactory;
        private readonly IServerRepository _serverRepository;

        public VersionController(IDbContextFactory dbContextFactory,
            IDataRepositoryFactory repositoryFactory, IServerRepository serverRepository)
        {
            _dbContextFactory = dbContextFactory;
            _repositoryFactory = repositoryFactory;
            _serverRepository = serverRepository;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<ServerData>> Get()
        {
            return Ok();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            var server = _serverRepository.GetAll().FirstOrDefault(s => s.ServerKey == id);
            if (server != null)
            {
                _dbContextFactory.Create(CreateConnectionString(server.ServerSource));
                var repo = _repositoryFactory.GetDataRepository<IVersionRepository>();
                return Ok(repo.GetByCode(string.Empty, string.Empty).VersionText);
            }
            return NotFound();
        }
        private string CreateConnectionString(string serverSource)
        {
            return $"Data Source={serverSource};User ID=QIQOMonitorUser;Password=QIQOMonitorUser;Application Name=QIQOMonitorAPI";
        }
    }
}
