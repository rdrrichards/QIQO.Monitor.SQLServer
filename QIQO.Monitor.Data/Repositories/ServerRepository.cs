using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace QIQO.Monitor.Data
{
    public class ServerRepository : RepositoryBase<ServerData>,
                                     IServerRepository
    {
        private readonly IMonitorDbContext _entityContext;
        public ServerRepository(IMonitorDbContext dbc, IServerMap map) : base(map)
        {
            _entityContext = dbc;
        }

        public override IEnumerable<ServerData> GetAll()
        {
            using (_entityContext) return MapRows(_entityContext.ExecuteProcedureAsSqlDataReader("monMonitoredServerGetAll"));
        }

        public override ServerData GetByID(int serverKey)
        {
            var pcol = new List<SqlParameter>() { Mapper.BuildParam("@serverKey", serverKey) };
            using (_entityContext) return MapRow(_entityContext.ExecuteProcedureAsSqlDataReader("monMonitoredServerGet", pcol));
        }

        public override void Insert(ServerData entity)
        {
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Save(ServerData entity)
        {
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Delete(ServerData entity)
        {
            using (_entityContext) _entityContext.ExecuteProcedureNonQuery("monMonitoredServerDelete", Mapper.MapParamsForDelete(entity));
        }

        public override void DeleteByID(int entityKey)
        {
            using (_entityContext) _entityContext.ExecuteProcedureNonQuery("monMonitoredServerDelete", Mapper.MapParamsForDelete(entityKey));
        }

        private void Upsert(ServerData entity)
        {
            using (_entityContext) _entityContext.ExecuteProcedureNonQuery("monMonitoredServerUpsert", Mapper.MapParamsForUpsert(entity));
        }
    }
}
