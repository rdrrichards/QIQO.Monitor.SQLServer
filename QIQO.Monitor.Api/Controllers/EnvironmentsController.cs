using Microsoft.AspNetCore.Mvc;
using QIQO.Monitor.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QIQO.Monitor.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnvironmentsController : ControllerBase
    {
        private readonly IEnvironmentManager _environmentManager;

        public EnvironmentsController(IEnvironmentManager environmentManager)
        {
            _environmentManager = environmentManager;
        }

        /// <summary>
        /// Get a collection of all Environments being managed
        /// </summary>
        /// <returns>200 - Ok</returns>
        /// <returns>500 - Internal Error</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Environment>> Get()
        {
            try
            {
                return Ok(_environmentManager.GetEnvironments());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        /// <summary>
        /// Get a Environment being managed
        /// </summary>
        /// <returns>200 - Ok</returns>
        /// <returns>404 - Not Found</returns>
        /// <returns>500 - Internal Error</returns>
        [HttpGet("{id}")]
        public ActionResult<Environment> Get(int id)
        {
            try
            {
                var environment = _environmentManager.GetEnvironments().FirstOrDefault(s => s.EnvironmentKey == id);
                if (environment != null)
                    return Ok(environment);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        /// <summary>
        /// Add a new Environment to be managed
        /// </summary>
        /// <returns>201 - Created</returns>
        /// <returns>400 - Bad Request</returns>
        /// <returns>500 - Internal Error</returns>
        [HttpPost()]
        public ActionResult<Environment> Post([FromBody] EnvironmentAdd environment)
        {
            if (environment == null) return BadRequest("Invalid environment parameter");
            try
            {
                var newEnv = _environmentManager.AddEnvironment(environment);
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
        /// Update an existing Environment being managed
        /// </summary>
        /// <returns>202 - Accepted</returns>
        /// <returns>400 - Bad Request</returns>
        /// <returns>500 - Internal Error</returns>
        [HttpPut("{id}")]
        public ActionResult<Environment> Put(int id, [FromBody] EnvironmentUpdate environment)
        {
            if (environment == null) return BadRequest("Invalid environment parameter");
            try
            {
                var newEnv = _environmentManager.UpdateEnvironment(id, environment);
                if (newEnv != null)
                    return Accepted(newEnv);
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Delete an existing Environment being managed
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
                _environmentManager.DeleteEnvironment(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
