using Microsoft.Extensions.Logging;
using QIQO.Monitor.Data;
using System.Collections.Generic;
using System.Linq;

namespace QIQO.Monitor.Client
{
    public interface IServiceManager
    {
        List<Service> GetServices();
        List<Service> GetServices(int serverKey);
        Service AddService(ServiceAdd environment);
        Service UpdateService(int environmentKey, ServiceUpdate environment);
        void DeleteService(int environmentKey);
    }
    public class ServiceManager : ManagerBase, IServiceManager
    {
        private readonly ICoreCacheService _cacheService;
        private readonly IServiceRepository _serviceRepository;
        private readonly IEnvironmentManager _environmentManager;
        private readonly IMonitorManager _monitorManager;

        public ServiceManager(ILogger<ServiceManager> logger, ICoreCacheService cacheService, IServiceRepository serviceRepository,
            IMonitorManager monitorManager, IEnvironmentManager environmentManager) : base(logger)
        {
            _cacheService = cacheService;
            _serviceRepository = serviceRepository;
            _monitorManager = monitorManager;
            _environmentManager = environmentManager;
        }
        public List<Service> GetServices()
        {
            return ExecuteOperation(() =>
            {
                var services = new List<Service>();
                var servicesToMonitor = _cacheService.GetServices().ToList();

                servicesToMonitor.ForEach(service =>
                {
                    services.Add(new Service(service, _monitorManager.GetMonitors(service.ServiceKey),
                        _environmentManager.GetServiceEnvironments(service.ServiceKey)));
                });

                return services;
            });
        }
        public List<Service> GetServices(int serverKey)
        {
            return ExecuteOperation(() =>
            {
                var services = new List<Service>();
                var servicesToMonitor = _cacheService.GetServices(serverKey).ToList();

                servicesToMonitor.ForEach(service =>
                {
                    services.Add(new Service(service, _monitorManager.GetMonitors(service.ServiceKey),
                        _environmentManager.GetServiceEnvironments(service.ServiceKey)));
                });

                return services;
            });
        }
        public Service AddService(ServiceAdd service)
        {
            return ExecuteOperation(() =>
            {
                var endData = new ServiceData { ServiceName = service.ServiceName };
                _serviceRepository.Insert(endData);
                _cacheService.RefreshCache();
                return GetServices().FirstOrDefault(e => e.ServiceName == service.ServiceName);
            });
        }
        public Service UpdateService(int serviceKey, ServiceUpdate service)
        {
            return ExecuteOperation(() =>
            {
                var endData = new ServiceData
                {
                    ServiceKey = serviceKey,
                    ServiceName = service.ServiceName,
                    InstanceName = service.InstanceName,
                    ServiceSource = service.ServiceSource,
                    ServiceTypeKey = service.ServiceTypeKey,
                    ServerKey = service.ServerKey
                };
                _serviceRepository.Save(endData);
                _cacheService.RefreshCache();
                return GetServices().FirstOrDefault(e => e.ServiceKey == serviceKey);
            });
        }
        public void DeleteService(int serviceKey)
        {
            ExecuteOperation(() =>
            {
                var endData = new ServiceData { ServiceKey = serviceKey };
                _serviceRepository.Delete(endData);
                _cacheService.RefreshCache();
            });
        }
    }
}