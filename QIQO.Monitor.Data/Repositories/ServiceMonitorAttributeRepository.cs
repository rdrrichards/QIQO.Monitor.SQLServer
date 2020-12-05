using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;

namespace QIQO.Monitor.Data
{
    public class ServiceMonitorAttributeRepository : RepositoryBase<ServiceMonitorAttributeData>,
                                     IServiceMonitorAttributeRepository
    {
        private readonly IMonitorDbContext _entityContext;
        public ServiceMonitorAttributeRepository(IMonitorDbContext dbc, IServiceMonitorAttributeMap map) : base(map)
        {
            _entityContext = dbc;
        }

        public override IEnumerable<ServiceMonitorAttributeData> GetAll()
        {
            using (_entityContext) return MapRows(_entityContext.ExecuteProcedureAsSqlDataReader("monMonitoredServiceMonitorAttributeGetAll"));
        }

        public override ServiceMonitorAttributeData GetByID(int monitor_key) =>
            throw new NotAllowedException("Selecting service monitor relationship data with this method is not allowed. Use GetAll(service_key, monitor_key) instead.");
        public IEnumerable<ServiceMonitorAttributeData> GetAll(int service_key, int monitor_key)
        {
            using (_entityContext) return MapRows(_entityContext.ExecuteProcedureAsSqlDataReader("monMonitoredServiceMonitorAttributeGet"));
        }
        public override void Insert(ServiceMonitorAttributeData entity)
        {
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Save(ServiceMonitorAttributeData entity)
        {
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Delete(ServiceMonitorAttributeData entity)
        {
            using (_entityContext) _entityContext.ExecuteProcedureNonQuery("monMonitoredServiceMonitorAttributeDelete", Mapper.MapParamsForDelete(entity));
        }

        public override void DeleteByID(int entityKey) =>
            throw new NotAllowedException("Deleting service monitor relationship data with this method is not allowed. Use Delete(ServiceMonitorAttributeData entity) instead.");

        private void Upsert(ServiceMonitorAttributeData entity)
        {
            using (_entityContext) _entityContext.ExecuteProcedureNonQuery("monMonitoredServiceMonitorAttributeUpsert", Mapper.MapParamsForUpsert(entity));
        }
    }
}
