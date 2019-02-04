using QIQO.Monitor.Core.Contracts;
using QIQO.Monitor.SQLServer.Data;

namespace QIQO.Monitor.Service
{
    public class Service : IModel
    {
        public Service(ServiceData serviceData)
        {
            ServiceKey = serviceData.ServiceKey;
            ServiceType = (ServiceType)serviceData.ServiceTypeKey;
            ServerKey = serviceData.ServerKey;
            ServiceName = serviceData.ServiceName;
            InstanceName = serviceData.InstanceName;
            ServiceSource = serviceData.ServiceSource;

        }
        public int ServiceKey { get; }
        public int ServerKey { get; }
        public string ServiceName { get; }
        public string InstanceName { get; }
        public string ServiceSource { get; }
        public ServiceType ServiceType { get; }
    }
}
