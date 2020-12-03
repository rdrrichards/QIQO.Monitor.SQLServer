using QIQO.Monitor.Core.Contracts;
using System.Collections.Generic;

namespace QIQO.Monitor.Service{
    public class Service: IModel
    {        
        public int ServiceKey { get; set; }
        public int ServerKey { get; set; }
        public string ServiceName { get; set; }
        public string InstanceName { get; set; }
        public string ServiceSource { get; set; }
        public ServiceType ServiceType { get; set; }
        public List<MonitorModel> Monitors { get; set; } = new List<MonitorModel>();
        public List<Environment> Environments { get; set; } = new List<Environment>();
    }
}
