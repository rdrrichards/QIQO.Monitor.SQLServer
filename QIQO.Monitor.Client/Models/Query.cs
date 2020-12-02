using QIQO.Monitor.Core.Contracts;
using QIQO.Monitor.Data;

namespace QIQO.Monitor.Client
{
    public class Query : IModel
    {
        public Query(QueryData queryData)
        {
            QueryKey = queryData.QueryKey;
            Name = queryData.Name;
            QueryText = queryData.QueryText;
        }
        public int QueryKey { get; }
        public string Name { get; }
        public string QueryText { get; }
    }
}
