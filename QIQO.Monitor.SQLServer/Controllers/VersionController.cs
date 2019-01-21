using System.Collections.Generic;
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

        public VersionController(IDbContextFactory dbContextFactory,
            IDataRepositoryFactory repositoryFactory, IServerRepository serverRepository) : base(dbContextFactory, repositoryFactory)
        {
            _serverRepository = serverRepository;
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
                return Ok(repo.Get().VersionText);
            }
            return NotFound();
        }
    }
}
