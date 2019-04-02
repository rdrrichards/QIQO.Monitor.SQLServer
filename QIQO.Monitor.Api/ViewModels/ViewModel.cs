using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QIQO.Monitor.Api
{
    public class ServerAdd
    {
        public string ServerName { get; set; }
        public List<ServiceAdd> Services { get; set; } = new List<ServiceAdd>();
        public List<EnvironmentAdd> Environments { get; set; } = new List<EnvironmentAdd>();
    }
    public class ServiceAdd
    {
        public string ServiceName { get; set; }
        public string InstanceName { get; set; }
        public string ServiceSource { get; set; }
    }
    public class EnvironmentAdd
    {
        public string EnvironmentName { get; set; }
    }
    public class ServerUpdate
    {
        public int ServerKey { get; set; }
        public string ServerName { get; set; }
        public List<ServiceUpdate> Services { get; set; } = new List<ServiceUpdate>();
        public List<EnvironmentUpdate> Environments { get; set; } = new List<EnvironmentUpdate>();
    }
    public class ServiceUpdate
    {
        public int ServiceKey { get; set; }
        public string ServiceName { get; set; }
        public string InstanceName { get; set; }
        public string ServiceSource { get; set; }
        public int ServiceTypeKey { get; set; }
        public int ServerKey { get; set; }
    }
    public class EnvironmentUpdate
    {
        public int EnvironmentKey { get; set; }
        public string EnvironmentName { get; set; }
    }
}
