using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace QIQO.Monitor.Data
{
    public class EnvironmentServiceRepository : RepositoryBase<EnvironmentServiceData>,
                                     IEnvironmentServiceRepository
    {
        private readonly IMonitorDbContext _entityContext;
        public EnvironmentServiceRepository(IMonitorDbContext dbc, IEnvironmentServiceMap map) : base(map)
        {
            _entityContext = dbc;
        }

        public override IEnumerable<EnvironmentServiceData> GetAll()
        {
            using (_entityContext) return MapRows(_entityContext.ExecuteProcedureAsSqlDataReader("monMonitorEnvironmentServiceGetAll"));
        }

        public override EnvironmentServiceData GetByID(int monitor_key)
        {
            var pcol = new List<SqlParameter>() { Mapper.BuildParam("@monitor_key", monitor_key) };
            using (_entityContext) return MapRow(_entityContext.ExecuteProcedureAsSqlDataReader("monMonitorEnvironmentServiceGet", pcol));
        }

        public override void Insert(EnvironmentServiceData entity)
        {
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Save(EnvironmentServiceData entity)
        {
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Delete(EnvironmentServiceData entity)
        {
            using (_entityContext) _entityContext.ExecuteProcedureNonQuery("monMonitorEnvironmentServiceDelete", Mapper.MapParamsForDelete(entity));
        }

        public override void DeleteByID(int entityKey)
        {
            using (_entityContext) _entityContext.ExecuteProcedureNonQuery("monMonitorEnvironmentServiceDelete", Mapper.MapParamsForDelete(entityKey));
        }

        private void Upsert(EnvironmentServiceData entity)
        {
            using (_entityContext) _entityContext.ExecuteProcedureNonQuery("monMonitorEnvironmentServiceUpsert", Mapper.MapParamsForUpsert(entity));
        }
    }
}
