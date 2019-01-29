using QIQO.Monitor.Core.Contracts;
using System;

namespace QIQO.Monitor.Domain
{
    public class MonitorResult
    {
        public DateTime ResultDateTime { get; } = DateTime.Now;
        public IResultPayload ResultPayload { get; }
    }
}
