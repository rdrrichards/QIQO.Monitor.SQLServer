using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace QIQO.Monitor.Data
{
    public class ServiceRepository : RepositoryBase<ServiceData>,
                                     IServiceRepository
    {
        private readonly IMonitorDbContext _entityContext;
        public ServiceRepository(IMonitorDbContext dbc, IServiceMap map) : base(map)
        {
            _entityContext = dbc;
        }

        public override IEnumerable<ServiceData> GetAll()
        {
            using (_entityContext) return MapRows(_entityContext.ExecuteProcedureAsSqlDataReader("monMonitoredServiceGetAll"));
        }

        public override ServiceData GetByID(int serviceKey)
        {
            var pcol = new List<SqlParameter>() { Mapper.BuildParam("@serviceKey", serviceKey) };
            using (_entityContext) return MapRow(_entityContext.ExecuteProcedureAsSqlDataReader("monMonitoredServiceGet", pcol));
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
            using (_entityContext) _entityContext.ExecuteProcedureNonQuery("monMonitoredServiceDelete", Mapper.MapParamsForDelete(entity));
        }

        public override void DeleteByID(int entityKey)
        {
            using (_entityContext) _entityContext.ExecuteProcedureNonQuery("monMonitoredServiceDelete", Mapper.MapParamsForDelete(entityKey));
        }

        private void Upsert(ServiceData entity)
        {
            using (_entityContext) _entityContext.ExecuteProcedureNonQuery("monMonitoredServiceUpsert", Mapper.MapParamsForUpsert(entity));
        }
    }
}
