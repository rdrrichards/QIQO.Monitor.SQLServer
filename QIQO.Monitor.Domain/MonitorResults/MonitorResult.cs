using QIQO.Monitor.Core.Contracts;
using System;
using System.Collections.Generic;

namespace QIQO.Monitor.Domain
{
    public interface IMonitorResult { }
    public interface IMonitorResult<T> : IMonitorResult where T : IModel
    {
        DateTime resultDateTime { get; }
        IEnumerable<T> results { get; set; }
    }
    public abstract class MonitorResult<T> : IMonitorResult<T> where T : IModel
    {
        public abstract ResultType resultType { get; }
        public virtual DateTime resultDateTime { get; } = DateTime.Now;
        public abstract IEnumerable<T> results { get; set; }
    }
    public enum ResultType
    {
        Health,
        Blocking,
        OpenTransaction,
        WaitStats
    }
}
