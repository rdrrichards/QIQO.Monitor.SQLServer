using Microsoft.Extensions.Logging;
using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;

namespace QIQO.Monitor.SQLServer.Data
{
    public class ServiceMonitorAttributeRepository : RepositoryBase<ServiceMonitorAttributeData>,
                                     IServiceMonitorAttributeRepository
    {
        private readonly IMonitorDbContext entityContext;
        public ServiceMonitorAttributeRepository(IMonitorDbContext dbc, IServiceMonitorAttributeMap map, ILogger<ServiceMonitorAttributeData> log) : base(log, map)
        {
            entityContext = dbc;
        }

        public override IEnumerable<ServiceMonitorAttributeData> GetAll()
        {
            Log.LogInformation("Accessing ServiceMonitorAttributeRepository GetAll function");
            using (entityContext) return MapRows(entityContext.ExecuteProcedureAsSqlDataReader("usp_service_monitor_attribute_all"));
        }

        public override ServiceMonitorAttributeData GetByID(int monitor_key) =>
            throw new NotAllowedException("Selecting service monitor relationship data with this method is not allowed. Use GetAll(service_key, monitor_key) instead.");
        public IEnumerable<ServiceMonitorAttributeData> GetAll(int service_key, int monitor_key)
        {
            Log.LogInformation("Accessing ServiceMonitorAttributeRepository GetAll function");
            using (entityContext) return MapRows(entityContext.ExecuteProcedureAsSqlDataReader("usp_service_monitor_attribute_get"));
        }
        public override void Insert(ServiceMonitorAttributeData entity)
        {
            Log.LogInformation("Accessing ServiceMonitorAttributeRepository Insert function");
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Save(ServiceMonitorAttributeData entity)
        {
            Log.LogInformation("Accessing ServiceMonitorAttributeRepository Save function");
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Delete(ServiceMonitorAttributeData entity)
        {
            Log.LogInformation("Accessing ServiceMonitorAttributeRepository Delete function");
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_service_monitor_attribute_del", Mapper.MapParamsForDelete(entity));
        }

        public override void DeleteByID(int entityKey) =>
            throw new NotAllowedException("Deleting service monitor relationship data with this method is not allowed. Use Delete(ServiceMonitorAttributeData entity) instead.");

        private void Upsert(ServiceMonitorAttributeData entity)
        {
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_service_monitor_attribute_ups", Mapper.MapParamsForUpsert(entity));
        }
    }
}
