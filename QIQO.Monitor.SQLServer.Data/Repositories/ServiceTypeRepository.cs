using Microsoft.Extensions.Logging;
using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace QIQO.Monitor.SQLServer.Data
{
    public class ServiceTypeRepository : RepositoryBase<ServiceTypeData>,
                                     IServiceTypeRepository
    {
        private readonly IMonitorDbContext entityContext;
        public ServiceTypeRepository(IMonitorDbContext dbc, IServiceTypeMap map, ILogger<ServiceTypeData> log) : base(log, map)
        {
            entityContext = dbc;
        }

        public override IEnumerable<ServiceTypeData> GetAll()
        {
            Log.LogInformation("Accessing ServiceTypeRepository GetAll function");
            using (entityContext) return MapRows(entityContext.ExecuteProcedureAsSqlDataReader("usp_server_all"));
        }

        public override ServiceTypeData GetByID(int server_key)
        {
            Log.LogInformation("Accessing ServiceTypeRepository GetByID function");
            var pcol = new List<SqlParameter>() { Mapper.BuildParam("@server_key", server_key) };
            using (entityContext) return MapRow(entityContext.ExecuteProcedureAsSqlDataReader("usp_server_get", pcol));
        }

        public override ServiceTypeData GetByCode(string server_code, string entityCode)
        {
            Log.LogInformation("Accessing ServiceTypeRepository GetByCode function");
            var pcol = new List<SqlParameter>() {
                Mapper.BuildParam("@server_code", server_code),
                Mapper.BuildParam("@company_code", entityCode)
            };
            using (entityContext) return MapRow(entityContext.ExecuteProcedureAsSqlDataReader("usp_server_get_c", pcol));
        }

        public override void Insert(ServiceTypeData entity)
        {
            Log.LogInformation("Accessing ServiceTypeRepository Insert function");
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Save(ServiceTypeData entity)
        {
            Log.LogInformation("Accessing ServiceTypeRepository Save function");
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Delete(ServiceTypeData entity)
        {
            Log.LogInformation("Accessing ServiceTypeRepository Delete function");
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_server_del", Mapper.MapParamsForDelete(entity));
        }

        public override void DeleteByCode(string entityCode)
        {
            Log.LogInformation("Accessing ServiceTypeRepository DeleteByCode function");
            var pcol = new List<SqlParameter>() { Mapper.BuildParam("@server_code", entityCode) };
            pcol.Add(Mapper.GetOutParam());
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_server_del_c", pcol);
        }

        public override void DeleteByID(int entityKey)
        {
            Log.LogInformation("Accessing ServiceTypeRepository Delete function");
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_server_del", Mapper.MapParamsForDelete(entityKey));
        }

        private void Upsert(ServiceTypeData entity)
        {
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_server_ups", Mapper.MapParamsForUpsert(entity));
        }
    }
}
