using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace QIQO.Monitor.Data
{
    public class ServiceTypeRepository : RepositoryBase<ServiceTypeData>,
                                     IServiceTypeRepository
    {
        private readonly IMonitorDbContext _entityContext;
        public ServiceTypeRepository(IMonitorDbContext dbc, IServiceTypeMap map) : base(map)
        {
            _entityContext = dbc;
        }

        public override IEnumerable<ServiceTypeData> GetAll()
        {
            using (_entityContext) return MapRows(_entityContext.ExecuteProcedureAsSqlDataReader("monServiceTypeGetAll"));
        }

        public override ServiceTypeData GetByID(int serviceTypeKey)
        {
            var pcol = new List<SqlParameter>() { Mapper.BuildParam("@ServiceType_key", serviceTypeKey) };
            using (_entityContext) return MapRow(_entityContext.ExecuteProcedureAsSqlDataReader("monServiceTypeGet", pcol));
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
            using (_entityContext) _entityContext.ExecuteProcedureNonQuery("monServiceTypeDelete", Mapper.MapParamsForDelete(entity));
        }

        public override void DeleteByID(int entityKey)
        {
            using (_entityContext) _entityContext.ExecuteProcedureNonQuery("monServiceTypeDelete", Mapper.MapParamsForDelete(entityKey));
        }

        private void Upsert(ServiceTypeData entity)
        {
            using (_entityContext) _entityContext.ExecuteProcedureNonQuery("monServiceTypeUpsert", Mapper.MapParamsForUpsert(entity));
        }
    }
}
