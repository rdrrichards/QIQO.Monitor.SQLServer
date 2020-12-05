using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QIQO.Monitor.Data
{
    public class MonitorMap : MapperBase, IMonitorMap
    {
        public MonitorData Map(IDataReader record)
        {
            try
            {
                return new MonitorData()
                {
                    MonitorKey = NullCheck<int>(record["MonitorKey"]),
                    MonitorTypeKey = NullCheck<int>(record["MonitorTypeKey"]),
                    MonitorName = NullCheck<string>(record["MonitorName"]),
                    LevelKey = NullCheck<int>(record["LevelKey"]),
                    CategoryKey = NullCheck<int>(record["CategoryKey"]),
                };
            }
            catch (Exception ex)
            {
                throw new MapException($"MonitorMap Exception occured: {ex.Message}", ex);
            }
        }

        public List<SqlParameter> MapParamsForUpsert(MonitorData entity) => new List<SqlParameter>
            {
                BuildParam("@MonitorKey", entity.MonitorKey),
                BuildParam("@MonitorTypeKey", entity.MonitorTypeKey),
                BuildParam("@MonitorName", entity.MonitorName),
                BuildParam("@LevelKey", entity.LevelKey),
                BuildParam("@CategoryKey", entity.CategoryKey),
                // GetOutParam()
            };

        public List<SqlParameter> MapParamsForDelete(MonitorData entity) => MapParamsForDelete(entity.MonitorKey);

        public List<SqlParameter> MapParamsForDelete(int category_key) => new List<SqlParameter>
            {
                BuildParam("@MonitorKey", category_key),
                // GetOutParam()
            };
    }
}
