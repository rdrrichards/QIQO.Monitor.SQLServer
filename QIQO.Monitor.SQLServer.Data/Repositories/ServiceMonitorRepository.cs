using Microsoft.Extensions.Logging;
using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;

namespace QIQO.Monitor.SQLServer.Data
{
    public class ServiceMonitorRepository : RepositoryBase<ServiceMonitorData>,
                                     IServiceMonitorRepository
    {
        private readonly IMonitorDbContext entityContext;
        public ServiceMonitorRepository(IMonitorDbContext dbc, IServiceMonitorMap map, ILogger<ServiceMonitorData> log) : base(log, map)
        {
            entityContext = dbc;
        }

        public override IEnumerable<ServiceMonitorData> GetAll()
        {
            Log.LogInformation("Accessing ServiceMonitorRepository GetAll function");
            using (entityContext) return MapRows(entityContext.ExecuteProcedureAsSqlDataReader("usp_service_monitor_all"));
        }

        public override ServiceMonitorData GetByID(int monitor_key) =>
            throw new NotAllowedException("Selecting service monitor relationship data with this method is not allowed. Use GetAll(service_key, monitor_key) instead.");
        public IEnumerable<ServiceMonitorData> GetAll(int service_key, int monitor_key)
        {
            Log.LogInformation("Accessing ServiceMonitorRepository GetAll function");
            using (entityContext) return MapRows(entityContext.ExecuteProcedureAsSqlDataReader("usp_service_monitor_get"));
        }
        public override void Insert(ServiceMonitorData entity)
        {
            Log.LogInformation("Accessing ServiceMonitorRepository Insert function");
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Save(ServiceMonitorData entity)
        {
            Log.LogInformation("Accessing ServiceMonitorRepository Save function");
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Delete(ServiceMonitorData entity)
        {
            Log.LogInformation("Accessing ServiceMonitorRepository Delete function");
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
