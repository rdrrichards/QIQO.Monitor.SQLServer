using QIQO.Monitor.Core.Contracts;
using QIQO.Monitor.SQLServer.Data;
using System.Collections.Generic;
using System.Linq;

namespace QIQO.Monitor.Api.Services
{
    public interface IServiceManager
    {
        List<Service> GetServices();
    }
    public class ServiceManager : IServiceManager
    {
        private readonly ICoreCacheService _cacheService;
        private readonly IQueryEntityService _queryEntityService;
        private readonly IEnvironmentEntityService _environmentEntityService;

        public ServiceManager(ICoreCacheService cacheService, IQueryEntityService queryEntityService, IEnvironmentEntityService environmentEntityService)
        {
            _cacheService = cacheService;
            _queryEntityService = queryEntityService;
            _environmentEntityService = environmentEntityService;
        }
        public List<Service> GetServices()
        {
            var services = new List<Service>();
            var servicesToMonitor = _cacheService.GetServices().ToList();

            servicesToMonitor.ForEach(service =>
            {
                var monitors = new List<Monitor>();
                _cacheService.GetMonitors(service.ServiceTypeKey).ToList().ForEach(monitor =>
                {
                    monitors.Add(new Monitor(monitor, _queryEntityService.Map(_cacheService.GetQueries(monitor.MonitorKey))));
                });
                services.Add(new Service(service, monitors, _environmentEntityService.Map(_cacheService.GetServiceEnvironments(service.ServiceKey))));
            });

            return services;
        }
    }
}