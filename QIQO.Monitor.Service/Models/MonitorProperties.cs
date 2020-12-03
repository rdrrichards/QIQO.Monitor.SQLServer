using QIQO.Monitor.Core.Contracts;

namespace QIQO.Monitor.Service
{
    public class MonitorProperty : IModel
    {
        public string PropertyType { get; set; }
        public string PropertyDataType { get; set; }
        public string PropertyValue { get; set; }
    }
}
