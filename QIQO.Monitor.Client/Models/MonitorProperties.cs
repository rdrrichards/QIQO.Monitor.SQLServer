using QIQO.Monitor.Core.Contracts;

namespace QIQO.Monitor.Client
{
    public class MonitorProperty : IModel
    {
        public MonitorProperty(string propertyType, string propertyDataType, string propertyValue)
        {
            PropertyType = propertyType;
            PropertyDataType = propertyDataType;
            PropertyValue = propertyValue;
        }
        public string PropertyType { get; }
        public string PropertyDataType { get; }
        public string PropertyValue { get; }
    }
}
