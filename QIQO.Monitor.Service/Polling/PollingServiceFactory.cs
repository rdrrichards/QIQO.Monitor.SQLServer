using Microsoft.Extensions.DependencyInjection;

namespace QIQO.Monitor.Service.Polling
{
    public interface IPollingServiceFactory
    {
        T GetPollingService<T>() where T : IPollingService;
    }
    public class PollingServiceFactory : IPollingServiceFactory
    {
        private IServiceCollection _services;

        public PollingServiceFactory(IServiceCollection services)
        {
            _services = services;
        }

        public T GetPollingService<T>() where T : IPollingService
        {
            var p = _services.BuildServiceProvider();
            return p.GetService<T>();
        }

    }
}
