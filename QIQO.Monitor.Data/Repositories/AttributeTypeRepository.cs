using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace QIQO.Monitor.Data
{
    public class AttributeTypeRepository : RepositoryBase<AttributeTypeData>,
                                     IAttributeTypeRepository
    {
        private readonly IMonitorDbContext _entityContext;
        public AttributeTypeRepository(IMonitorDbContext dbc, IAttributeTypeMap map) : base(map)
        {
            _entityContext = dbc;
        }

        public override IEnumerable<AttributeTypeData> GetAll()
        {
            using (_entityContext) return MapRows(_entityContext.ExecuteProcedureAsSqlDataReader("monAttributeTypeGetAll"));
        }

        public override AttributeTypeData GetByID(int attributeTypeKey)
        {
            var pcol = new List<SqlParameter>() { Mapper.BuildParam("@AttributeTypeKey", attributeTypeKey) };
            using (_entityContext) return MapRow(_entityContext.ExecuteProcedureAsSqlDataReader("monAttributeTypeGet", pcol));
        }

        public override void Insert(AttributeTypeData entity)
        {
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Save(AttributeTypeData entity)
        {
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Delete(AttributeTypeData entity)
        {
            using (_entityContext) _entityContext.ExecuteProcedureNonQuery("monAttributeTypeDelete", Mapper.MapParamsForDelete(entity));
        }

        public override void DeleteByID(int entityKey)
        {
            using (_entityContext) _entityContext.ExecuteProcedureNonQuery("monAttributeTypeDelete", Mapper.MapParamsForDelete(entityKey));
        }

        private void Upsert(AttributeTypeData entity)
        {
            using (_entityContext) _entityContext.ExecuteProcedureNonQuery("monAttributeTypeUpsert", Mapper.MapParamsForUpsert(entity));
        }
    }

}
