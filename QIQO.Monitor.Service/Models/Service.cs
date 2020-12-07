using QIQO.Monitor.Core.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace QIQO.Monitor.Service{
    public class Service: IModel
    {        
        public int ServiceKey { get; set; }
        public int ServerKey { get; set; }
        public string ServiceName { get; set; }
        public string InstanceName { get; set; }
        public string ServiceSource { get; set; }
        public ServiceType ServiceType { get; set; }
        public IEnumerable<MonitorModel> Monitors { get; set; } = Enumerable.Empty<MonitorModel>();
        public IEnumerable<Environment> Environments { get; set; } = Enumerable.Empty<Environment>();
    }
}
