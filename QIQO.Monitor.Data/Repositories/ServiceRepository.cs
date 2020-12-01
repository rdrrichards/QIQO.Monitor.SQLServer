using Microsoft.Extensions.Logging;
using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace QIQO.Monitor.Data
{
    public class ServiceRepository : RepositoryBase<ServiceData>,
                                     IServiceRepository
    {
        private readonly IMonitorDbContext entityContext;
        public ServiceRepository(IMonitorDbContext dbc, IServiceMap map) : base(map)
        {
            entityContext = dbc;
        }

        public override IEnumerable<ServiceData> GetAll()
        {
            using (entityContext) return MapRows(entityContext.ExecuteProcedureAsSqlDataReader("usp_service_all"));
        }

        public override ServiceData GetByID(int service_key)
        {
            var pcol = new List<SqlParameter>() { Mapper.BuildParam("@service_key", service_key) };
            using (entityContext) return MapRow(entityContext.ExecuteProcedureAsSqlDataReader("usp_service_get", pcol));
        }

        public override void Insert(ServiceData entity)
        {
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Save(ServiceData entity)
        {
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Delete(ServiceData entity)
        {
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_service_del", Mapper.MapParamsForDelete(entity));
        }

        public override void DeleteByID(int entityKey)
        {
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_service_del", Mapper.MapParamsForDelete(entityKey));
        }

        private void Upsert(ServiceData entity)
        {
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_service_ups", Mapper.MapParamsForUpsert(entity));
        }
    }
}
