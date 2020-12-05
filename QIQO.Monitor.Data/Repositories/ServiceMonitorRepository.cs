using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;

namespace QIQO.Monitor.Data
{
    public class ServiceMonitorRepository : RepositoryBase<ServiceMonitorData>,
                                     IServiceMonitorRepository
    {
        private readonly IMonitorDbContext _entityContext;
        public ServiceMonitorRepository(IMonitorDbContext dbc, IServiceMonitorMap map) : base(map)
        {
            _entityContext = dbc;
        }

        public override IEnumerable<ServiceMonitorData> GetAll()
        {
            using (_entityContext) return MapRows(_entityContext.ExecuteProcedureAsSqlDataReader("monMonitoredServiceMonitorGetAll"));
        }

        public override ServiceMonitorData GetByID(int monitor_key) =>
            throw new NotAllowedException("Selecting service monitor relationship data with this method is not allowed. Use GetAll(service_key, monitor_key) instead.");
        public IEnumerable<ServiceMonitorData> GetAll(int service_key, int monitor_key)
        {
            using (_entityContext) return MapRows(_entityContext.ExecuteProcedureAsSqlDataReader("monMonitoredServiceMonitorGet"));
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
            using (_entityContext) _entityContext.ExecuteProcedureNonQuery("monMonitoredServiceMonitorDelete", Mapper.MapParamsForDelete(entity));
        }

        public override void DeleteByID(int entityKey) =>
            throw new NotAllowedException("Deleting service monitor relationship data with this method is not allowed. Use Delete(ServiceMonitorData entity) instead.");

        private void Upsert(ServiceMonitorData entity)
        {
            using (_entityContext) _entityContext.ExecuteProcedureNonQuery("monMonitoredServiceMonitorUpsert", Mapper.MapParamsForUpsert(entity));
        }
    }
}
