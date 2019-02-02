using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QIQO.Monitor.SQLServer.Data
{
    public class QueryMap : MapperBase, IQueryMap
    {
        public QueryData Map(IDataReader record)
        {
            try
            {
                return new QueryData()
                {
                    QueryKey = NullCheck<int>(record["query_key"]),
                    Name = NullCheck<string>(record["name"]),
                    LevelKey = NullCheck<int>(record["level_key"]),
                    CategoryKey = NullCheck<int>(record["category_key"]),
                    QueryText = NullCheck<string>(record["query_text"])
                };
            }
            catch (Exception ex)
            {
                throw new MapException($"QueryMap Exception occured: {ex.Message}", ex);
            }
        }

        public List<SqlParameter> MapParamsForUpsert(QueryData entity) => new List<SqlParameter>
            {
                BuildParam("@query_key", entity.QueryKey),
                BuildParam("@name", entity.Name),
                BuildParam("@level_key", entity.LevelKey),
                BuildParam("@category_key", entity.CategoryKey),
                BuildParam("@query_text", entity.QueryText),
                GetOutParam()
            };

        public List<SqlParameter> MapParamsForDelete(QueryData entity) => MapParamsForDelete(entity.QueryKey);

        public List<SqlParameter> MapParamsForDelete(int query_key) => new List<SqlParameter>
            {
                BuildParam("@query_key", query_key),
                GetOutParam()
            };
    }
}
