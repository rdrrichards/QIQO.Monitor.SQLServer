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

        public ServiceManager(ICoreCacheService cacheService)
        {
            _cacheService = cacheService;
        }
        public List<Service> GetServices()
        {
            var services = new List<Service>();
            var servicesToMonitor = _cacheService.GetServices().ToList();

            servicesToMonitor.ForEach(service =>
            {
                services.Add(new Service(service));
            });

            return services;
        }
    }
}