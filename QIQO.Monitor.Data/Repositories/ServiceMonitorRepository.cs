using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;

namespace QIQO.Monitor.Data
{
    public class ServiceMonitorRepository : RepositoryBase<ServiceMonitorData>,
                                     IServiceMonitorRepository
    {
        private readonly IMonitorDbContext entityContext;
        public ServiceMonitorRepository(IMonitorDbContext dbc, IServiceMonitorMap map) : base(map)
        {
            entityContext = dbc;
        }

        public override IEnumerable<ServiceMonitorData> GetAll()
        {
            using (entityContext) return MapRows(entityContext.ExecuteProcedureAsSqlDataReader("usp_service_monitor_all"));
        }

        public override ServiceMonitorData GetByID(int monitor_key) =>
            throw new NotAllowedException("Selecting service monitor relationship data with this method is not allowed. Use GetAll(service_key, monitor_key) instead.");
        public IEnumerable<ServiceMonitorData> GetAll(int service_key, int monitor_key)
        {
            using (entityContext) return MapRows(entityContext.ExecuteProcedureAsSqlDataReader("usp_service_monitor_get"));
        }
        public override void Insert(ServiceMonitorData entity)
        {
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Save(ServiceMonitorData entity)
        {
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Delete(ServiceMonitorData entity)
        {
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_service_monitor_del", Mapper.MapParamsForDelete(entity));
        }

        public override void DeleteByID(int entityKey) =>
            throw new NotAllowedException("Deleting service monitor relationship data with this method is not allowed. Use Delete(ServiceMonitorData entity) instead.");

        private void Upsert(ServiceMonitorData entity)
        {
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_service_monitor_ups", Mapper.MapParamsForUpsert(entity));
        }
    }
}
