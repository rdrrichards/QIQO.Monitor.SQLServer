using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using QIQO.Monitor.SQLServer.Data;

namespace QIQO.Monitor.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlockingController : QIQOControllerBase
    {
        private readonly IServerRepository _serverRepository;

        public BlockingController(IDbContextFactory dbContextFactory, 
            IDataRepositoryFactory repositoryFactory, IServerRepository serverRepository) : base(dbContextFactory, repositoryFactory)
        {
            _serverRepository = serverRepository;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<BlockingData>> Get(int id)
        {
            var server = _serverRepository.GetAll().FirstOrDefault(s => s.ServerKey == id);
            if (server != null)
            {
                //***** THIS IS NOT RIGHT!! *******//
                CreateContext(server.ServerName);
                var repo = _repositoryFactory.GetDataRepository<IBlockingRepository>();
                var blockingData = repo.Get();
                // _hubClientService.SendResult(ResultType.Version, version);
                return Ok(blockingData);
            }
            return NotFound();
        }
    }
}
