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
            using (entityContext) return MapRows(entityContext.ExecuteProcedureAsSqlDataReader("usp_service_type_all"));
        }

        public override ServiceTypeData GetByID(int service_type_key)
        {
            Log.LogInformation("Accessing ServiceTypeRepository GetByID function");
            var pcol = new List<SqlParameter>() { Mapper.BuildParam("@service_type_key", service_type_key) };
            using (entityContext) return MapRow(entityContext.ExecuteProcedureAsSqlDataReader("usp_service_type_get", pcol));
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
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_service_type_del", Mapper.MapParamsForDelete(entity));
        }

        public override void DeleteByID(int entityKey)
        {
            Log.LogInformation("Accessing ServiceTypeRepository Delete function");
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_service_type_del", Mapper.MapParamsForDelete(entityKey));
        }

        private void Upsert(ServiceTypeData entity)
        {
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_service_type_ups", Mapper.MapParamsForUpsert(entity));
        }
    }
}
