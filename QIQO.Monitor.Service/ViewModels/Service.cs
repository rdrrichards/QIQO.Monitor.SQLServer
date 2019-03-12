using QIQO.Monitor.Core.Contracts;
using QIQO.Monitor.SQLServer.Data;
using System.Collections.Generic;

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
        public Service(ServiceData serviceData, List<Monitor> monitors, List<Environment> environments) : this(serviceData)
        {
            Monitors = monitors;
            Environments = environments;
        }
        public int ServiceKey { get; }
        public int ServerKey { get; }
        public string ServiceName { get; }
        public string InstanceName { get; }
        public string ServiceSource { get; }
        public ServiceType ServiceType { get; }
        public List<Monitor> Monitors { get; } = new List<Monitor>();
        public List<Environment> Environments { get; } = new List<Environment>();
    }
}
