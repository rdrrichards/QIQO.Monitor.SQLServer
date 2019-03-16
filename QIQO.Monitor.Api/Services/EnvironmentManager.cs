using QIQO.Monitor.SQLServer.Data;
using System.Collections.Generic;
using System.Linq;

namespace QIQO.Monitor.Api.Services
{
    public interface IEnvironmentManager
    {
        List<Environment> GetEnvironments();
    }
    public class EnvironmentManager : IEnvironmentManager
    {
        private readonly ICoreCacheService _cacheService;

        public EnvironmentManager(ICoreCacheService cacheService)
        {
            _cacheService = cacheService;
        }
        public List<Environment> GetEnvironments()
        {
            var environments = new List<Environment>();
            var environmentsToMonitor = _cacheService.GetEnviroments().ToList();

            environmentsToMonitor.ForEach(environment =>
            {
                environments.Add(new Environment(environment));
            });

            return environments;
        }
    }
}
