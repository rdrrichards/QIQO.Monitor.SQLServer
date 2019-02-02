using QIQO.Monitor.Core.Contracts;
using System;
using System.Collections.Generic;

namespace QIQO.Monitor.Domain
{
    public interface IMonitorResult { }
    public interface IMonitorResult<T> : IMonitorResult where T : IModel
    {
        DateTime ResultDateTime { get; }
        List<T> Results { get; }
    }
    public abstract class MonitorResult<T> : IMonitorResult<T> where T : IModel
    {
        public DateTime ResultDateTime { get; } = DateTime.Now;
        // public IResultPayload ResultPayload { get; }
        public abstract List<T> Results { get; }
    }
}
