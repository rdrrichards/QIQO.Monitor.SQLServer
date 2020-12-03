using QIQO.Monitor.Core.Contracts;

namespace QIQO.Monitor.Service
{
    public class Query : IModel
    {
        public int QueryKey { get; set; }
        public string Name { get; set; }
        public string QueryText { get; set; }
    }
}
