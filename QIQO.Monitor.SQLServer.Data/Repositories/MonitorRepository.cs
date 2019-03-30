using Microsoft.Extensions.Logging;
using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace QIQO.Monitor.SQLServer.Data
{
    public class MonitorRepository : RepositoryBase<MonitorData>,
                                     IMonitorRepository
    {
        private readonly IMonitorDbContext entityContext;
        public MonitorRepository(IMonitorDbContext dbc, IMonitorMap map, ILogger<MonitorData> log) : base(log, map)
        {
            entityContext = dbc;
        }

        public override IEnumerable<MonitorData> GetAll()
        {
            Log.LogInformation("Accessing MonitorRepository GetAll function");
            using (entityContext) return MapRows(entityContext.ExecuteProcedureAsSqlDataReader("usp_monitor_all"));
        }

        public override MonitorData GetByID(int monitor_key)
        {
            Log.LogInformation("Accessing MonitorRepository GetByID function");
            var pcol = new List<SqlParameter>() { Mapper.BuildParam("@monitor_key", monitor_key) };
            using (entityContext) return MapRow(entityContext.ExecuteProcedureAsSqlDataReader("usp_monitor_get", pcol));
        }

        public override void Insert(MonitorData entity)
        {
            Log.LogInformation("Accessing MonitorRepository Insert function");
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Save(MonitorData entity)
        {
            Log.LogInformation("Accessing MonitorRepository Save function");
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Delete(MonitorData entity)
        {
            Log.LogInformation("Accessing MonitorRepository Delete function");
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_monitor_del", Mapper.MapParamsForDelete(entity));
        }
        
        public override void DeleteByID(int entityKey)
        {
            Log.LogInformation("Accessing MonitorRepository Delete function");
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_monitor_del", Mapper.MapParamsForDelete(entityKey));
        }

        private void Upsert(MonitorData entity)
        {
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_monitor_ups", Mapper.MapParamsForUpsert(entity));
        }
    }
}
