using QIQO.Monitor.SQLServer.Data;

namespace QIQO.Monitor.Api
{
    public class ServiceEntityService : IServiceEntityService
    {
        public Service Map(ServiceData ent) => new Service(ent);

        public ServiceData Map(Service ent) => new ServiceData {
            ServiceKey = ent.ServiceKey,
            ServiceName = ent.ServiceName,
            InstanceName = ent.InstanceName,
            ServiceSource = ent.ServiceSource,
            ServerKey = ent.ServerKey,
            ServiceTypeKey = (int)ent.ServiceType
        };
    }
}
