﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using QIQO.Monitor.SQLServer.Data;

namespace QIQO.Monitor.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpenTranactionController : QIQOControllerBase
    {
        private readonly IServerRepository _serverRepository;

        public OpenTranactionController(IDbContextFactory dbContextFactory, 
            IDataRepositoryFactory repositoryFactory, IServerRepository serverRepository) : base(dbContextFactory, repositoryFactory)
        {
            _serverRepository = serverRepository;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<OpenTranactionData>> Get(int id)
        {
            var server = _serverRepository.GetAll().FirstOrDefault(s => s.ServerKey == id);
            if (server != null)
            {
                //***** THIS IS NOT RIGHT!! *******//
                CreateContext(server.ServerName);
                var repo = _repositoryFactory.GetDataRepository<IOpenTranactionRepository>();
                var transOpen = repo.Get();
                // _hubClientService.SendResult(ResultType.Version, version);
                return Ok(transOpen);
            }
            return NotFound();
        }
    }
}
