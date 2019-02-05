using Microsoft.Extensions.Logging;
using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace QIQO.Monitor.SQLServer.Data
{
    public class MonitorQueryRepository : RepositoryBase<MonitorQueryData>,
                                     IMonitorQueryRepository
    {
        private readonly IMonitorDbContext entityContext;
        public MonitorQueryRepository(IMonitorDbContext dbc, IMonitorQueryMap map, ILogger<MonitorQueryData> log) : base(log, map)
        {
            entityContext = dbc;
        }

        public override IEnumerable<MonitorQueryData> GetAll()
        {
            Log.LogInformation("Accessing MonitorRepository GetAll function");
            using (entityContext) return MapRows(entityContext.ExecuteProcedureAsSqlDataReader("usp_monitor_query_all"));
        }

        public override MonitorQueryData GetByID(int monitor_key)
        {
            Log.LogInformation("Accessing MonitorRepository GetByID function");
            var pcol = new List<SqlParameter>() { Mapper.BuildParam("@monitor_key", monitor_key) };
            using (entityContext) return MapRow(entityContext.ExecuteProcedureAsSqlDataReader("usp_monitor_query_get", pcol));
        }

        public override MonitorQueryData GetByCode(string monitor_code, string entityCode)
        {
            Log.LogInformation("Accessing MonitorRepository GetByCode function");
            var pcol = new List<SqlParameter>() {
                Mapper.BuildParam("@monitor_code", monitor_code),
                Mapper.BuildParam("@company_code", entityCode)
            };
            using (entityContext) return MapRow(entityContext.ExecuteProcedureAsSqlDataReader("usp_monitor_query_get_c", pcol));
        }

        public override void Insert(MonitorQueryData entity)
        {
            Log.LogInformation("Accessing MonitorRepository Insert function");
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Save(MonitorQueryData entity)
        {
            Log.LogInformation("Accessing MonitorRepository Save function");
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Delete(MonitorQueryData entity)
        {
            Log.LogInformation("Accessing MonitorRepository Delete function");
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_monitor_query_del", Mapper.MapParamsForDelete(entity));
        }

        public override void DeleteByCode(string entityCode)
        {
            Log.LogInformation("Accessing MonitorRepository DeleteByCode function");
            var pcol = new List<SqlParameter>() { Mapper.BuildParam("@monitor_code", entityCode) };
            pcol.Add(Mapper.GetOutParam());
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_monitor_query_del_c", pcol);
        }

        public override void DeleteByID(int entityKey)
        {
            Log.LogInformation("Accessing MonitorRepository Delete function");
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_monitor_query_del", Mapper.MapParamsForDelete(entityKey));
        }

        private void Upsert(MonitorQueryData entity)
        {
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_monitor_query_ups", Mapper.MapParamsForUpsert(entity));
        }
    }
}
