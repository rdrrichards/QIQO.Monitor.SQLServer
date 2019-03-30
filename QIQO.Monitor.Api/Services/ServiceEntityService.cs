using QIQO.Monitor.SQLServer.Data;
using System;

namespace QIQO.Monitor.Api
{
    public class ServiceEntityService : IServiceEntityService
    {
        public Service Map(ServiceData ent) => new Service(ent);

        public ServiceData Map(Service ent) => throw new NotImplementedException();
    }
}
