using Microsoft.Extensions.Logging;
using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace QIQO.Monitor.SQLServer.Data
{
    public class MonitorTypeRepository : RepositoryBase<MonitorTypeData>,
                                     IMonitorTypeRepository
    {
        private readonly IMonitorDbContext entityContext;
        public MonitorTypeRepository(IMonitorDbContext dbc, IMonitorTypeMap map, ILogger<MonitorTypeData> log) : base(log, map)
        {
            entityContext = dbc;
        }

        public override IEnumerable<MonitorTypeData> GetAll()
        {
            Log.LogInformation("Accessing MonitorTypeRepository GetAll function");
            using (entityContext) return MapRows(entityContext.ExecuteProcedureAsSqlDataReader("usp_monitor_type_all"));
        }

        public override MonitorTypeData GetByID(int monitor_type_key)
        {
            Log.LogInformation("Accessing MonitorTypeRepository GetByID function");
            var pcol = new List<SqlParameter>() { Mapper.BuildParam("@monitor_type_key", monitor_type_key) };
            using (entityContext) return MapRow(entityContext.ExecuteProcedureAsSqlDataReader("usp_monitor_type_get", pcol));
        }

        public override MonitorTypeData GetByCode(string monitor_type_code, string entityCode)
        {
            Log.LogInformation("Accessing MonitorTypeRepository GetByCode function");
            var pcol = new List<SqlParameter>() {
                Mapper.BuildParam("@monitor_type_code", monitor_type_code),
                Mapper.BuildParam("@company_code", entityCode)
            };
            using (entityContext) return MapRow(entityContext.ExecuteProcedureAsSqlDataReader("usp_monitor_type_get_c", pcol));
        }

        public override void Insert(MonitorTypeData entity)
        {
            Log.LogInformation("Accessing MonitorTypeRepository Insert function");
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Save(MonitorTypeData entity)
        {
            Log.LogInformation("Accessing MonitorTypeRepository Save function");
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Delete(MonitorTypeData entity)
        {
            Log.LogInformation("Accessing MonitorTypeRepository Delete function");
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_monitor_type_del", Mapper.MapParamsForDelete(entity));
        }

        public override void DeleteByCode(string entityCode)
        {
            Log.LogInformation("Accessing MonitorTypeRepository DeleteByCode function");
            var pcol = new List<SqlParameter>() { Mapper.BuildParam("@monitor_type_code", entityCode) };
            pcol.Add(Mapper.GetOutParam());
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_monitor_type_del_c", pcol);
        }

        public override void DeleteByID(int entityKey)
        {
            Log.LogInformation("Accessing MonitorTypeRepository Delete function");
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_monitor_type_del", Mapper.MapParamsForDelete(entityKey));
        }

        private void Upsert(MonitorTypeData entity)
        {
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_monitor_type_ups", Mapper.MapParamsForUpsert(entity));
        }
    }
}
