using QIQO.Monitor.Core.Contracts;
using QIQO.Monitor.SQLServer.Data;

namespace QIQO.Monitor.Api
{
    public class Query : IModel
    {
        public Query(QueryData queryData)
        {
            QueryKey = queryData.QueryKey;
            Name = queryData.Name;
            QueryLevel = (QueryLevel)queryData.LevelKey;
            QueryCategory = (QueryCategory)queryData.CategoryKey;
            QueryText = queryData.QueryText;
        }
        public int QueryKey { get; }
        public string Name { get; }
        public QueryLevel QueryLevel { get; }
        public QueryCategory QueryCategory { get; }
        public string QueryText { get; }
    }
}
