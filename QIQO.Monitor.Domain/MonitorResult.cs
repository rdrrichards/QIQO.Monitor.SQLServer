using System;

namespace QIQO.Monitor.Domain
{
    public class MonitorResult
    {
        public DateTime ResultDateTime { get; } = DateTime.Now;
        public IResultPayload ResultPayload { get; }
    }

    public interface IResultPayload { }
    public class BlockingResult : IResultPayload
    {

    }
    public class OpenTransactionResult : IResultPayload
    {

    }
}
