namespace QIQO.Monitor.Domain
{
    public class Service
    {
        public Service(string name) => Name = name;
        public Service(string name, ServiceType serviceType) : this(name) => ServiceType = serviceType;
        public string Name { get; }
        public ServiceType ServiceType { get; } = ServiceType.Unknown;
    }
}
