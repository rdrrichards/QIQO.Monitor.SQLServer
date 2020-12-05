using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QIQO.Monitor.Data
{
    public class MonitorQueryMap : MapperBase, IMonitorQueryMap
    {
        public MonitorQueryData Map(IDataReader record)
        {
            try
            {
                return new MonitorQueryData()
                {
                    MonitorKey = NullCheck<int>(record["MonitorKey"]),
                    QueryKey = NullCheck<int>(record["QueryKey"]),
                };
            }
            catch (Exception ex)
            {
                throw new MapException($"MonitorQueryMap Exception occured: {ex.Message}", ex);
            }
        }

        public List<SqlParameter> MapParamsForUpsert(MonitorQueryData entity) => new List<SqlParameter>
            {
                BuildParam("@MonitorKey", entity.MonitorKey),
                BuildParam("@QueryKey", entity.QueryKey),
                // GetOutParam()
            };

        public List<SqlParameter> MapParamsForDelete(MonitorQueryData entity) => new List<SqlParameter>
            {
                BuildParam("@MonitorKey", entity.MonitorKey),
                BuildParam("@QueryKey", entity.QueryKey),
                // GetOutParam()
            };

        public List<SqlParameter> MapParamsForDelete(int category_key) => throw new NotImplementedException();
    }
}
