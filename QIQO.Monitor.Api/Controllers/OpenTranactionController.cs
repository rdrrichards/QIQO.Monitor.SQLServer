﻿using Microsoft.AspNetCore.Mvc;
using QIQO.Monitor.Api.Services;
using QIQO.Monitor.SQLServer.Data;
using System.Collections.Generic;
using System.Linq;

namespace QIQO.Monitor.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpenTransactionController : QIQOControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public OpenTransactionController(IDbContextFactory dbContextFactory,
            IDataRepositoryFactory repositoryFactory, IServiceManager serviceManager) : base(dbContextFactory, repositoryFactory)
        {
            _serviceManager = serviceManager;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<OpenTransactionData>> Get(int id)
        {
            try
            {
                var server = _serviceManager.GetServices().FirstOrDefault(s => s.ServiceKey == id);
                if (server != null)
                {
                    var monitor = server.Monitors.FirstOrDefault(m => m.MonitorType == MonitorType.SqlServer &&
                        m.MonitorCategory == MonitorCategories.OpenTranactions);
                    var query = monitor.Queries.FirstOrDefault();
                    CreateContext(server.ServiceSource);
                    var repo = _repositoryFactory.GetDataRepository<IOpenTransactionRepository>();
                    var transOpen = repo.Get(query.QueryText);
                    return Ok(transOpen);
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
