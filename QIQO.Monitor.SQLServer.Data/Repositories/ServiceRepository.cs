using Microsoft.Extensions.Logging;
using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace QIQO.Monitor.SQLServer.Data
{
    public class ServiceRepository : RepositoryBase<ServiceData>,
                                     IServiceRepository
    {
        private readonly IMonitorDbContext entityContext;
        public ServiceRepository(IMonitorDbContext dbc, IServiceMap map, ILogger<ServiceData> log) : base(log, map)
        {
            entityContext = dbc;
        }

        public override IEnumerable<ServiceData> GetAll()
        {
            Log.LogInformation("Accessing ServiceRepository GetAll function");
            using (entityContext) return MapRows(entityContext.ExecuteProcedureAsSqlDataReader("usp_server_all"));
        }

        public override ServiceData GetByID(int server_key)
        {
            Log.LogInformation("Accessing ServiceRepository GetByID function");
            var pcol = new List<SqlParameter>() { Mapper.BuildParam("@server_key", server_key) };
            using (entityContext) return MapRow(entityContext.ExecuteProcedureAsSqlDataReader("usp_server_get", pcol));
        }

        public override ServiceData GetByCode(string server_code, string entityCode)
        {
            Log.LogInformation("Accessing ServiceRepository GetByCode function");
            var pcol = new List<SqlParameter>() {
                Mapper.BuildParam("@server_code", server_code),
                Mapper.BuildParam("@company_code", entityCode)
            };
            using (entityContext) return MapRow(entityContext.ExecuteProcedureAsSqlDataReader("usp_server_get_c", pcol));
        }

        public override void Insert(ServiceData entity)
        {
            Log.LogInformation("Accessing ServiceRepository Insert function");
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Save(ServiceData entity)
        {
            Log.LogInformation("Accessing ServiceRepository Save function");
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Delete(ServiceData entity)
        {
            Log.LogInformation("Accessing ServiceRepository Delete function");
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_server_del", Mapper.MapParamsForDelete(entity));
        }

        public override void DeleteByCode(string entityCode)
        {
            Log.LogInformation("Accessing ServiceRepository DeleteByCode function");
            var pcol = new List<SqlParameter>() { Mapper.BuildParam("@server_code", entityCode) };
            pcol.Add(Mapper.GetOutParam());
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_server_del_c", pcol);
        }

        public override void DeleteByID(int entityKey)
        {
            Log.LogInformation("Accessing ServiceRepository Delete function");
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_server_del", Mapper.MapParamsForDelete(entityKey));
        }

        private void Upsert(ServiceData entity)
        {
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_server_ups", Mapper.MapParamsForUpsert(entity));
        }
    }
}
