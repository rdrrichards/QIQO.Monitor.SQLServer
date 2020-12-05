using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QIQO.Monitor.Data
{
    public class QueryHistoryMap : MapperBase, IQueryHistoryMap
    {
        public QueryHistoryData Map(IDataReader record)
        {
            try
            {
                return new QueryHistoryData()
                {
                    QueryHistoryKey = NullCheck<int>(record["QueryHistoryKey"]),
                    MonitorKey = NullCheck<int>(record["MonitorKey"]),
                    QueryKey = NullCheck<int>(record["QueryKey"]),
                    ServiceKey = NullCheck<int>(record["ServiceKey"]),
                    ResultText = NullCheck<string>(record["ResultText"])
                };
            }
            catch (Exception ex)
            {
                throw new MapException($"QueryHistoryMap Exception occured: {ex.Message}", ex);
            }
        }

        public List<SqlParameter> MapParamsForUpsert(QueryHistoryData entity) => new List<SqlParameter>
            {
                BuildParam("@QueryHistoryKey", entity.QueryHistoryKey),
                BuildParam("@MonitorKey", entity.MonitorKey),
                BuildParam("@QueryKey", entity.QueryKey),
                BuildParam("@ServiceKey", entity.ServiceKey),
                BuildParam("@ResultText", entity.ResultText),
                // GetOutParam()
            };

        public List<SqlParameter> MapParamsForDelete(QueryHistoryData entity) => MapParamsForDelete(entity.QueryHistoryKey);

        public List<SqlParameter> MapParamsForDelete(int category_key) => new List<SqlParameter>
            {
                BuildParam("@QueryHistoryKey", category_key),
                // GetOutParam()
            };
    }
}
