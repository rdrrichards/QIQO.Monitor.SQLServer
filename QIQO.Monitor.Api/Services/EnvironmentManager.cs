using Microsoft.Extensions.Logging;
using QIQO.Monitor.Core.Contracts;
using QIQO.Monitor.SQLServer.Data;
using System.Collections.Generic;
using System.Linq;

namespace QIQO.Monitor.Api.Services
{
    public interface IEnvironmentManager
    {
        List<Environment> GetEnvironments();
        List<Environment> GetServiceEnvironments(int serviceKey);
        List<Environment> GetServerEnvironments(int serviceKey);
        Environment AddEnvironment(EnvironmentAdd environment);
        Environment UpdateEnvironment(int environmentKey, EnvironmentUpdate environment);
        void DeleteEnvironment(int environmentKey);
    }
    public class EnvironmentManager : ManagerBase, IEnvironmentManager
    {
        private readonly ICoreCacheService _cacheService;
        private readonly IEnvironmentRepository _environmentRepository;
        private readonly IEnvironmentEntityService _environmentEntityService;

        public EnvironmentManager(ILogger<EnvironmentManager> logger, ICoreCacheService cacheService,
            IEnvironmentRepository environmentRepository, IEnvironmentEntityService environmentEntityService) : base(logger)
        {
            _cacheService = cacheService;
            _environmentRepository = environmentRepository;
            _environmentEntityService = environmentEntityService;
        }
        public List<Environment> GetEnvironments()
        {
            return ExecuteOperation(() =>
            {
                return _environmentEntityService.Map(_cacheService.GetEnvironments());
            });
        }
        public List<Environment> GetServiceEnvironments(int serviceKey)
        {
            return ExecuteOperation(() =>
            {
                return _environmentEntityService.Map(_cacheService.GetServiceEnvironments(serviceKey));
            });
        }
        public List<Environment> GetServerEnvironments(int serviceKey)
        {
            return ExecuteOperation(() =>
            {
                return _environmentEntityService.Map(_cacheService.GetServiceEnvironments(serviceKey));
            });
        }
        public Environment AddEnvironment(EnvironmentAdd environment)
        {
            return ExecuteOperation(() =>
            {
                var endData = new EnvironmentData { EnvironmentName = environment.EnvironmentName };
                _environmentRepository.Insert(endData);
                _cacheService.RefreshCache();
                return GetEnvironments().FirstOrDefault(e => e.EnvironmentName == environment.EnvironmentName);
            });

        }
        public Environment UpdateEnvironment(int environmentKey, EnvironmentUpdate environment)
        {
            return ExecuteOperation(() =>
            {
                var endData = new EnvironmentData { EnvironmentKey = environmentKey, EnvironmentName = environment.EnvironmentName };
                _environmentRepository.Save(endData);
                _cacheService.RefreshCache();
                return GetEnvironments().FirstOrDefault(e => e.EnvironmentKey == environmentKey);
            });

        }
        public void DeleteEnvironment(int environmentKey)
        {
            ExecuteOperation(() =>
            {
                var endData = new EnvironmentData { EnvironmentKey = environmentKey };
                _environmentRepository.Delete(endData);
                _cacheService.RefreshCache();
            });
        }
    }
}
