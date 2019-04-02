using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using QIQO.Monitor.Api.Services;

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
        [HttpGet]
        public ActionResult<IEnumerable<Environment>> Get()
        {
            return Ok(_environmentManager.GetEnvironments());
        }


        /// <summary>
        /// Get a Environment being managed
        /// </summary>
        /// <returns>200 - Ok</returns>
        [HttpGet("{id}")]
        public ActionResult<Environment> Get(int id)
        {
            var environment = _environmentManager.GetEnvironments().FirstOrDefault(s => s.EnvironmentKey == id);
            if (environment != null)
                return Ok(environment);
            else
                return NotFound();
        }


        /// <summary>
        /// Add a new Environment to be managed
        /// </summary>
        /// <returns>200 - Ok</returns>
        [HttpPost()]
        public ActionResult<Environment> Post([FromBody] EnvironmentAdd environment)
        {
            if (environment == null) return BadRequest("Invalid environment parameter");

            var newEnv = _environmentManager.AddEnvironment(environment);
            if (newEnv != null)
                return Created("", newEnv);
            else
                return BadRequest();
        }

        /// <summary>
        /// Update an existing Environment being managed
        /// </summary>
        /// <returns>200 - Ok</returns>
        [HttpPut("{id}")]
        public ActionResult<Environment> Put(int id, [FromBody] EnvironmentUpdate environment)
        {
            if (environment == null) return BadRequest("Invalid environment parameter");

            var newEnv = _environmentManager.UpdateEnvironment(id, environment);
            if (newEnv != null)
                return Created("", newEnv);
            else
                return BadRequest();
        }

        /// <summary>
        /// Update an existing Environment being managed
        /// </summary>
        /// <returns>200 - Ok</returns>
        [HttpDelete("{id}")]
        public ActionResult<Environment> Delete(int id)
        {
            if (id == 0) return BadRequest("Invalid id parameter");

            _environmentManager.DeleteEnvironment(id);
            return NoContent();
        }
    }
}
