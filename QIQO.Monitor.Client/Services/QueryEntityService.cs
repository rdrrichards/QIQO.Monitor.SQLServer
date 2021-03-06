﻿using QIQO.Monitor.Data;

namespace QIQO.Monitor.Client
{
    public class QueryEntityService : IQueryEntityService
    {
        public Query Map(QueryData ent) => new Query(ent);

        public QueryData Map(Query ent) => new QueryData
        {
            QueryKey = ent.QueryKey,
            Name = ent.Name,
            QueryText = ent.QueryText
        };
    }
}
