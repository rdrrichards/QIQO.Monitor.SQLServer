using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace QIQO.Monitor.Data
{
    public class EnvironmentServerRepository : RepositoryBase<EnvironmentServerData>,
                                     IEnvironmentServerRepository
    {
        private readonly IMonitorDbContext _entityContext;
        public EnvironmentServerRepository(IMonitorDbContext dbc, IEnvironmentServerMap map) : base(map)
        {
            _entityContext = dbc;
        }

        public override IEnumerable<EnvironmentServerData> GetAll()
        {
            using (_entityContext) return MapRows(_entityContext.ExecuteProcedureAsSqlDataReader("monMonitorEnvironmentServerGetAll"));
        }

        public override EnvironmentServerData GetByID(int monitor_key)
        {
            var pcol = new List<SqlParameter>() { Mapper.BuildParam("@monitor_key", monitor_key) };
            using (_entityContext) return MapRow(_entityContext.ExecuteProcedureAsSqlDataReader("monMonitorEnvironmentServerGet", pcol));
        }

        public override void Insert(EnvironmentServerData entity)
        {
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Save(EnvironmentServerData entity)
        {
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Delete(EnvironmentServerData entity)
        {
            using (_entityContext) _entityContext.ExecuteProcedureNonQuery("monMonitorEnvironmentServerDelete", Mapper.MapParamsForDelete(entity));
        }

        public override void DeleteByID(int entityKey)
        {
            using (_entityContext) _entityContext.ExecuteProcedureNonQuery("monMonitorEnvironmentServerDelete", Mapper.MapParamsForDelete(entityKey));
        }

        private void Upsert(EnvironmentServerData entity)
        {
            using (_entityContext) _entityContext.ExecuteProcedureNonQuery("monMonitorEnvironmentServerUpsert", Mapper.MapParamsForUpsert(entity));
        }
    }
}
