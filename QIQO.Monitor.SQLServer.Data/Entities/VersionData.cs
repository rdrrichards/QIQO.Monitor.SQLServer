using QIQO.Monitor.Core.Contracts;

namespace QIQO.Monitor.SQLServer.Data
{
    public class VersionData : IEntity
    {
        public string VersionText { get; set; }
    }
}