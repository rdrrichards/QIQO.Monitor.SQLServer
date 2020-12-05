using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace QIQO.Monitor.Data
{
    public class AttributeDataTypeRepository : RepositoryBase<AttributeDataTypeData>,
                                    IAttributeDataTypeRepository
    {
        private readonly IMonitorDbContext _entityContext;
        public AttributeDataTypeRepository(IMonitorDbContext dbc, IAttributeDataTypeMap map) : base(map)
        {
            _entityContext = dbc;
        }

        public override IEnumerable<AttributeDataTypeData> GetAll()
        {
            using (_entityContext) return MapRows(_entityContext.ExecuteProcedureAsSqlDataReader("monAttributeDataTypeGetAll"));
        }

        public override AttributeDataTypeData GetByID(int attributeDataTypeKey)
        {
            var pcol = new List<SqlParameter>() { Mapper.BuildParam("@AttributeDataTypeKey", attributeDataTypeKey) };
            using (_entityContext) return MapRow(_entityContext.ExecuteProcedureAsSqlDataReader("monAttributeDataTypeGet", pcol));
        }

        public override void Insert(AttributeDataTypeData entity)
        {
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Save(AttributeDataTypeData entity)
        {
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Delete(AttributeDataTypeData entity)
        {
            using (_entityContext) _entityContext.ExecuteProcedureNonQuery("monAttributeDataTypeDelete", Mapper.MapParamsForDelete(entity));
        }

        public override void DeleteByID(int entityKey)
        {
            using (_entityContext) _entityContext.ExecuteProcedureNonQuery("monAttributeDataTypeDelete", Mapper.MapParamsForDelete(entityKey));
        }

        private void Upsert(AttributeDataTypeData entity)
        {
            using (_entityContext) _entityContext.ExecuteProcedureNonQuery("monAttributeDataTypeUpsert", Mapper.MapParamsForUpsert(entity));
        }
    }

}
