using QIQO.Monitor.Core.Contracts;
using QIQO.Monitor.SQLServer.Data;

namespace QIQO.Monitor.Service
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
