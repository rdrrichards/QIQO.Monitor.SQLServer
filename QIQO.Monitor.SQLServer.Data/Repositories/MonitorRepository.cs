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
            using (entityContext) return MapRows(entityContext.ExecuteProcedureAsSqlDataReader("usp_server_all"));
        }

        public override MonitorData GetByID(int server_key)
        {
            Log.LogInformation("Accessing MonitorRepository GetByID function");
            var pcol = new List<SqlParameter>() { Mapper.BuildParam("@server_key", server_key) };
            using (entityContext) return MapRow(entityContext.ExecuteProcedureAsSqlDataReader("usp_server_get", pcol));
        }

        public override MonitorData GetByCode(string server_code, string entityCode)
        {
            Log.LogInformation("Accessing MonitorRepository GetByCode function");
            var pcol = new List<SqlParameter>() {
                Mapper.BuildParam("@server_code", server_code),
                Mapper.BuildParam("@company_code", entityCode)
            };
            using (entityContext) return MapRow(entityContext.ExecuteProcedureAsSqlDataReader("usp_server_get_c", pcol));
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
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_server_del", Mapper.MapParamsForDelete(entity));
        }

        public override void DeleteByCode(string entityCode)
        {
            Log.LogInformation("Accessing MonitorRepository DeleteByCode function");
            var pcol = new List<SqlParameter>() { Mapper.BuildParam("@server_code", entityCode) };
            pcol.Add(Mapper.GetOutParam());
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_server_del_c", pcol);
        }

        public override void DeleteByID(int entityKey)
        {
            Log.LogInformation("Accessing MonitorRepository Delete function");
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_server_del", Mapper.MapParamsForDelete(entityKey));
        }

        private void Upsert(MonitorData entity)
        {
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_server_ups", Mapper.MapParamsForUpsert(entity));
        }
    }
}
