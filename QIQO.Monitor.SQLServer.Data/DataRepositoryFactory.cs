using Microsoft.Extensions.DependencyInjection;
using QIQO.Monitor.Core.Contracts;

namespace QIQO.Monitor.SQLServer.Data
{
    public interface IDataRepositoryFactory
    {
        T GetDataRepository<T>() where T : IRepository;
    }
    public class DataRepositoryFactory : IDataRepositoryFactory
    {
        private IServiceCollection _services;

        public DataRepositoryFactory(IServiceCollection services)
        {
            _services = services;
        }

        public T GetDataRepository<T>() where T : IRepository
        {
            var p = _services.BuildServiceProvider();
            return p.GetService<T>();
        }

    }
}
