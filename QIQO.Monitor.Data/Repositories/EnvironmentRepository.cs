using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace QIQO.Monitor.Data
{
    public class EnvironmentRepository : RepositoryBase<EnvironmentData>,
                                     IEnvironmentRepository
    {
        private readonly IMonitorDbContext _entityContext;
        public EnvironmentRepository(IMonitorDbContext dbc, IEnvironmentMap map) : base(map)
        {
            _entityContext = dbc;
        }

        public override IEnumerable<EnvironmentData> GetAll()
        {
            using (_entityContext) return MapRows(_entityContext.ExecuteProcedureAsSqlDataReader("monMonitorEnvironmentGetAll"));
        }

        public override EnvironmentData GetByID(int environmentKey)
        {
            var pcol = new List<SqlParameter>() { Mapper.BuildParam("@monitorEnvironmentKey", environmentKey) };
            using (_entityContext) return MapRow(_entityContext.ExecuteProcedureAsSqlDataReader("monMonitorEnvironmentGet", pcol));
        }

        public override void Insert(EnvironmentData entity)
        {
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Save(EnvironmentData entity)
        {
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Delete(EnvironmentData entity)
        {
            using (_entityContext) _entityContext.ExecuteProcedureNonQuery("monMonitorEnvironmentDelete", Mapper.MapParamsForDelete(entity));
        }

        public override void DeleteByID(int entityKey)
        {
            using (_entityContext) _entityContext.ExecuteProcedureNonQuery("monMonitorEnvironmentDelete", Mapper.MapParamsForDelete(entityKey));
        }

        private void Upsert(EnvironmentData entity)
        {
            using (_entityContext) _entityContext.ExecuteProcedureNonQuery("monMonitorEnvironmentUpsert", Mapper.MapParamsForUpsert(entity));
        }
    }
}
