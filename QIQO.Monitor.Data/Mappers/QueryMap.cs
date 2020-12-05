using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QIQO.Monitor.Data
{
    public class QueryMap : MapperBase, IQueryMap
    {
        public QueryData Map(IDataReader record)
        {
            try
            {
                return new QueryData()
                {
                    QueryKey = NullCheck<int>(record["QueryKey"]),
                    Name = NullCheck<string>(record["Name"]),
                    QueryText = NullCheck<string>(record["QueryText"])
                };
            }
            catch (Exception ex)
            {
                throw new MapException($"QueryMap Exception occured: {ex.Message}", ex);
            }
        }

        public List<SqlParameter> MapParamsForUpsert(QueryData entity) => new List<SqlParameter>
            {
                BuildParam("@QueryKey", entity.QueryKey),
                BuildParam("@Name", entity.Name),
                BuildParam("@QueryText", entity.QueryText),
                // GetOutParam()
            };

        public List<SqlParameter> MapParamsForDelete(QueryData entity) => MapParamsForDelete(entity.QueryKey);

        public List<SqlParameter> MapParamsForDelete(int query_key) => new List<SqlParameter>
            {
                BuildParam("@QueryKey", query_key),
                // GetOutParam()
            };
    }
}
