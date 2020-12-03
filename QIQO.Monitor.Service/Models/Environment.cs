using QIQO.Monitor.Core.Contracts;
using QIQO.Monitor.Data;

namespace QIQO.Monitor.Service
{
    public class Environment : IModel
    {
        public int EnvironmentKey { get; set; }
        public string EnvironmentName { get; set; }

    }
}
