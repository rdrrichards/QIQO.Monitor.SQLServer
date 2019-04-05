﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using QIQO.Monitor.Api.Services;
using QIQO.Monitor.SQLServer.Data;

namespace QIQO.Monitor.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueriesController : ControllerBase
    {
        private readonly IQueryManager _serverManager;

        public QueriesController(IQueryManager serverManager)
        {
            _serverManager = serverManager;
        }

        /// <summary>
        /// Get a collection of all Querys being managed
        /// </summary>
        /// <returns>200 - Ok</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Query>> Get()
        {
            return Ok(_serverManager.GetQueries());
        }


        /// <summary>
        /// Get a Query being managed
        /// </summary>
        /// <returns>200 - Ok</returns>
        [HttpGet("{id}")]
        public ActionResult<Query> Get(int id)
        {
            var server = _serverManager.GetQueries().FirstOrDefault(s => s.QueryKey == id);
            if (server != null)
                return Ok(server);
            else
                return NotFound();
        }


        /// <summary>
        /// Add a new Query to be managed
        /// </summary>
        /// <returns>200 - Ok</returns>
        [HttpPost()]
        public ActionResult<Query> Post([FromBody] QueryAdd server)
        {
            if (server == null) return BadRequest("Invalid server parameter");

            var newEnv = _serverManager.AddQuery(server);
            if (newEnv != null)
                return Created("", newEnv);
            else
                return BadRequest();
        }

        /// <summary>
        /// Update an existing Query being managed
        /// </summary>
        /// <returns>200 - Ok</returns>
        [HttpPut("{id}")]
        public ActionResult<Query> Put(int id, [FromBody] QueryUpdate server)
        {
            if (server == null) return BadRequest("Invalid server parameter");

            var newEnv = _serverManager.UpdateQuery(id, server);
            if (newEnv != null)
                return Created("", newEnv);
            else
                return BadRequest();
        }

        /// <summary>
        /// Delete an existing Query being managed
        /// </summary>
        /// <returns>200 - Ok</returns>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (id == 0) return BadRequest("Invalid id parameter");

            _serverManager.DeleteQuery(id);
            return NoContent();
        }
    }
}
