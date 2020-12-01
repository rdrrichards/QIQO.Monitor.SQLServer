using Microsoft.Extensions.DependencyInjection;
using QIQO.Monitor.Core.Contracts;
using System;

namespace QIQO.Monitor.SQLServer.Data
{
    public interface IDataRepositoryFactory
    {
        T GetDataRepository<T>() where T : IRepository;
    }
    public class DataRepositoryFactory : IDataRepositoryFactory
    {
        private IServiceProvider _serviceProviders;

        public DataRepositoryFactory(IServiceProvider serviceProviders)
        {
            _serviceProviders = serviceProviders;
        }

        public T GetDataRepository<T>() where T : IRepository
        {
            //var p = _services.BuildServiceProvider();
            return _serviceProviders.GetService<T>();
            // return default;
        }

    }
}
