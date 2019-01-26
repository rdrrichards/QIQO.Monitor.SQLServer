using System.Linq;
using Microsoft.AspNetCore.Mvc;
using QIQO.Monitor.SQLServer.Data;

namespace QIQO.Monitor.SQLServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VersionController : QIQOControllerBase
    {
        private readonly IServerRepository _serverRepository;
        private readonly IHubClientService _hubClientService;

        public VersionController(IDbContextFactory dbContextFactory, IHubClientService hubClientService,
            IDataRepositoryFactory repositoryFactory, IServerRepository serverRepository) : base(dbContextFactory, repositoryFactory)
        {
            _serverRepository = serverRepository;
            _hubClientService = hubClientService;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            var server = _serverRepository.GetAll().FirstOrDefault(s => s.ServerKey == id);
            if (server != null)
            {
                CreateContext(server.ServerSource);
                var repo = _repositoryFactory.GetDataRepository<IVersionRepository>();
                var version = repo.Get().ToArray()[0].VersionText;
                _hubClientService.SendResult(ResultType.Version, version);
                return Ok(version);
            }
            return NotFound();
        }
    }
}
