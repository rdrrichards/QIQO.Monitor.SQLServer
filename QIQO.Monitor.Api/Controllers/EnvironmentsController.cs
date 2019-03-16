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
            // var environment = _environmentManager.GetEnvironments().FirstOrDefault(s => s.EnvironmentKey == id);
            if (environment != null)
                return Ok(environment);
            else
                return NotFound();
        }

        /// <summary>
        /// Update an existing Environment being managed
        /// </summary>
        /// <returns>200 - Ok</returns>
        [HttpPut("{id}")]
        public ActionResult<Environment> Put(int id, [FromBody] EnvironmentUpdate environment)
        {
            // var environment = _environmentManager.GetEnvironments().FirstOrDefault(s => s.EnvironmentKey == id);
            if (environment != null)
                return Ok(environment);
            else
                return NotFound();
        }
    }
}
