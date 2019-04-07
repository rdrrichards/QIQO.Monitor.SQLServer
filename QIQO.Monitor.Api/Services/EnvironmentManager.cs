using Microsoft.Extensions.Logging;
using QIQO.Monitor.SQLServer.Data;
using System.Collections.Generic;
using System.Linq;

namespace QIQO.Monitor.Api.Services
{
    public interface IEnvironmentManager
    {
        List<Environment> GetEnvironments();
        Environment AddEnvironment(EnvironmentAdd environment);
        Environment UpdateEnvironment(int environmentKey, EnvironmentUpdate environment);
        void DeleteEnvironment(int environmentKey);
    }
    public class EnvironmentManager : ManagerBase, IEnvironmentManager
    {
        private readonly ICoreCacheService _cacheService;
        private readonly IEnvironmentRepository _environmentRepository;

        public EnvironmentManager(ILogger<EnvironmentManager> logger, ICoreCacheService cacheService,
            IEnvironmentRepository environmentRepository) : base(logger)
        {
            _cacheService = cacheService;
            _environmentRepository = environmentRepository;
        }
        public List<Environment> GetEnvironments()
        {
            return ExecuteOperation(() => {
                var environments = new List<Environment>();
                var environmentsToMonitor = _cacheService.GetEnviroments().ToList();

                environmentsToMonitor.ForEach(environment =>
                {
                    environments.Add(new Environment(environment));
                });

                return environments;
            });
        }
        public Environment AddEnvironment(EnvironmentAdd environment)
        {
            return ExecuteOperation(() => {
                var endData = new EnvironmentData { EnvironmentName = environment.EnvironmentName };
                _environmentRepository.Insert(endData);
                _cacheService.RefreshCache();
                return GetEnvironments().FirstOrDefault(e => e.EnvironmentName == environment.EnvironmentName);
            });
            
        }
        public Environment UpdateEnvironment(int environmentKey, EnvironmentUpdate environment)
        {
            return ExecuteOperation(() => {
                var endData = new EnvironmentData { EnvironmentKey = environmentKey, EnvironmentName = environment.EnvironmentName };
                _environmentRepository.Save(endData);
                _cacheService.RefreshCache();
                return GetEnvironments().FirstOrDefault(e => e.EnvironmentKey == environmentKey);
            });
            
        }
        public void DeleteEnvironment(int environmentKey)
        {
            ExecuteOperation(() => {
                var endData = new EnvironmentData { EnvironmentKey = environmentKey };
                _environmentRepository.Delete(endData);
                _cacheService.RefreshCache();
            });
        }
    }
}
