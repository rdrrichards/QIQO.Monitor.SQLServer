using QIQO.Monitor.SQLServer.Data;
using System;

namespace QIQO.Monitor.Api
{
    public class QueryEntityService : IQueryEntityService
    {
        public Query Map(QueryData ent) => new Query(ent);

        public QueryData Map(Query ent) => throw new NotImplementedException();
    }
}
