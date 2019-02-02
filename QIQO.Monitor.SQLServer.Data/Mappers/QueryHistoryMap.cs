using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QIQO.Monitor.SQLServer.Data
{
    public class QueryHistoryMap : MapperBase, IQueryHistoryMap
    {
        public QueryHistoryData Map(IDataReader record)
        {
            try
            {
                return new QueryHistoryData()
                {
                    QueryHistoryKey = NullCheck<int>(record["query_history_key"]),
                    MonitorKey = NullCheck<int>(record["monitor_key"]),
                    QueryKey = NullCheck<int>(record["query_key"]),
                    ServiceKey = NullCheck<int>(record["service_key"]),
                    ResultText = NullCheck<string>(record["result_text"])
                };
            }
            catch (Exception ex)
            {
                throw new MapException($"QueryHistoryMap Exception occured: {ex.Message}", ex);
            }
        }

        public List<SqlParameter> MapParamsForUpsert(QueryHistoryData entity) => new List<SqlParameter>
            {
                BuildParam("@query_history_key", entity.QueryHistoryKey),
                BuildParam("@monitor_key", entity.MonitorKey),
                BuildParam("@query_key", entity.QueryKey),
                BuildParam("@service_key", entity.ServiceKey),
                BuildParam("@result_text", entity.ResultText),
                GetOutParam()
            };

        public List<SqlParameter> MapParamsForDelete(QueryHistoryData entity) => MapParamsForDelete(entity.QueryHistoryKey);

        public List<SqlParameter> MapParamsForDelete(int category_key) => new List<SqlParameter>
            {
                BuildParam("@category_key", category_key),
                GetOutParam()
            };
    }
}
