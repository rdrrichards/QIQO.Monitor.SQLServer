using QIQO.Monitor.Core.Contracts;
using QIQO.Monitor.SQLServer.Data;
using System.Collections.Generic;
using System.Linq;

namespace QIQO.Monitor.Api.Services
{
    public interface IServiceManager
    {
        List<Service> GetServices();
        Service AddService(ServiceAdd environment);
        Service UpdateService(int environmentKey, ServiceUpdate environment);
        void DeleteService(int environmentKey);
    }
    public class ServiceManager : IServiceManager
    {
        private readonly ICoreCacheService _cacheService;
        private readonly IQueryEntityService _queryEntityService;
        private readonly IEnvironmentEntityService _environmentEntityService;
        private readonly IServiceRepository _serviceRepository;

        public ServiceManager(ICoreCacheService cacheService, IQueryEntityService queryEntityService,
            IEnvironmentEntityService environmentEntityService, IServiceRepository serviceRepository)
        {
            _cacheService = cacheService;
            _queryEntityService = queryEntityService;
            _environmentEntityService = environmentEntityService;
            _serviceRepository = serviceRepository;
        }
        public List<Service> GetServices()
        {
            var services = new List<Service>();
            var servicesToMonitor = _cacheService.GetServices().ToList();

            servicesToMonitor.ForEach(service =>
            {
                var monitors = new List<Monitor>();
                _cacheService.GetActiveServiceMonitors(service.ServiceKey).ToList().ForEach(monitor =>
                {
                    monitors.Add(new Monitor(monitor, _queryEntityService.Map(_cacheService.GetQueries(monitor.MonitorKey))));
                });
                services.Add(new Service(service, monitors, _environmentEntityService.Map(_cacheService.GetServiceEnvironments(service.ServiceKey))));
            });

            return services;
        }
        public Service AddService(ServiceAdd service)
        {
            var endData = new ServiceData { ServiceName = service.ServiceName };
            _serviceRepository.Insert(endData);
            _cacheService.RefreshCache();
            return GetServices().FirstOrDefault(e => e.ServiceName == service.ServiceName);
        }
        public Service UpdateService(int serviceKey, ServiceUpdate service)
        {
            var endData = new ServiceData { ServiceKey = serviceKey,
                ServiceName = service.ServiceName,
                InstanceName = service.InstanceName,
                ServiceSource = service.ServiceSource,
                ServiceTypeKey = service.ServiceTypeKey,
                ServerKey = service.ServerKey
            };
            _serviceRepository.Save(endData);
            _cacheService.RefreshCache();
            return GetServices().FirstOrDefault(e => e.ServiceKey == serviceKey);
        }
        public void DeleteService(int serviceKey)
        {
            var endData = new ServiceData { ServiceKey = serviceKey };
            _serviceRepository.Delete(endData);
            _cacheService.RefreshCache();
        }
    }
}