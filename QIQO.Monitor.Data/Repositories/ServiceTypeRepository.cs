using Microsoft.Extensions.Logging;
using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace QIQO.Monitor.Data
{
    public class ServiceTypeRepository : RepositoryBase<ServiceTypeData>,
                                     IServiceTypeRepository
    {
        private readonly IMonitorDbContext entityContext;
        public ServiceTypeRepository(IMonitorDbContext dbc, IServiceTypeMap map) : base(map)
        {
            entityContext = dbc;
        }

        public override IEnumerable<ServiceTypeData> GetAll()
        {
            using (entityContext) return MapRows(entityContext.ExecuteProcedureAsSqlDataReader("usp_service_type_all"));
        }

        public override ServiceTypeData GetByID(int service_type_key)
        {
            var pcol = new List<SqlParameter>() { Mapper.BuildParam("@service_type_key", service_type_key) };
            using (entityContext) return MapRow(entityContext.ExecuteProcedureAsSqlDataReader("usp_service_type_get", pcol));
        }

        public override void Insert(ServiceTypeData entity)
        {
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Save(ServiceTypeData entity)
        {
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Delete(ServiceTypeData entity)
        {
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_service_type_del", Mapper.MapParamsForDelete(entity));
        }

        public override void DeleteByID(int entityKey)
        {
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_service_type_del", Mapper.MapParamsForDelete(entityKey));
        }

        private void Upsert(ServiceTypeData entity)
        {
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_service_type_ups", Mapper.MapParamsForUpsert(entity));
        }
    }
}
