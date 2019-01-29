using QIQO.Monitor.Core.Contracts;
using System.Collections.Generic;

namespace QIQO.Monitor.Domain
{
    public interface IResultPayload
    {
        IEnumerable<IModel> Results { get; }
    }
    public interface IResultPayload<T> : IResultPayload where T : class, IModel, new() { }
}
