using Microsoft.AspNetCore.Mvc;
using QIQO.Monitor.Client;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QIQO.Monitor.Client
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueriesController : ControllerBase
    {
        private readonly IQueryManager _queryManager;

        public QueriesController(IQueryManager queryManager)
        {
            _queryManager = queryManager;
        }

        /// <summary>
        /// Get a collection of all Querys being managed
        /// </summary>
        /// <returns>200 - Ok</returns>
        /// <returns>500 - Internal Error</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Query>> Get()
        {
            try
            {
                return Ok(_queryManager.GetQueries());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        /// <summary>
        /// Get a Query being managed
        /// </summary>
        /// <returns>200 - Ok</returns>
        /// <returns>404 - Not Found</returns>
        /// <returns>500 - Internal Error</returns>
        [HttpGet("{id}")]
        public ActionResult<Query> Get(int id)
        {
            try
            {
                var query = _queryManager.GetQueries().FirstOrDefault(s => s.QueryKey == id);
                if (query != null)
                    return Ok(query);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        /// <summary>
        /// Add a new Query to be managed
        /// </summary>
        /// <returns>201 - Created</returns>
        /// <returns>400 - Bad Request</returns>
        /// <returns>500 - Internal Error</returns>
        [HttpPost()]
        public ActionResult<Query> Post([FromBody] QueryAdd query)
        {
            if (query == null) return BadRequest("Invalid query parameter");
            try
            {
                var newEnv = _queryManager.AddQuery(query);
                if (newEnv != null)
                    return Created("", newEnv);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Update an existing Query being managed
        /// </summary>
        /// <returns>202 - Accepted</returns>
        /// <returns>400 - Bad Request</returns>
        /// <returns>500 - Internal Error</returns>
        [HttpPut("{id}")]
        public ActionResult<Query> Put(int id, [FromBody] QueryUpdate query)
        {
            if (query == null) return BadRequest("Invalid query parameter");

            try
            {
                var newEnv = _queryManager.UpdateQuery(id, query);
                if (newEnv != null)
                    return Accepted("", newEnv);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Delete an existing Query being managed
        /// </summary>
        /// <returns>204 - No Content</returns>
        /// <returns>400 - Bad Request</returns>
        /// <returns>500 - Internal Error</returns>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (id == 0) return BadRequest("Invalid id parameter");

            try
            {
                _queryManager.DeleteQuery(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
