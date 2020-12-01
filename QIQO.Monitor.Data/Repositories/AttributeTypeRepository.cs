using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace QIQO.Monitor.Data
{
    public class AttributeTypeRepository : RepositoryBase<AttributeTypeData>,
                                     IAttributeTypeRepository
    {
        private readonly IMonitorDbContext entityContext;
        public AttributeTypeRepository(IMonitorDbContext dbc, IAttributeTypeMap map) : base(map)
        {
            entityContext = dbc;
        }

        public override IEnumerable<AttributeTypeData> GetAll()
        {
            using (entityContext) return MapRows(entityContext.ExecuteProcedureAsSqlDataReader("usp_attribute_type_all"));
        }

        public override AttributeTypeData GetByID(int attribute_type_key)
        {
            var pcol = new List<SqlParameter>() { Mapper.BuildParam("@attribute_type_key", attribute_type_key) };
            using (entityContext) return MapRow(entityContext.ExecuteProcedureAsSqlDataReader("usp_attribute_type_get", pcol));
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
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_attribute_type_del", Mapper.MapParamsForDelete(entity));
        }

        public override void DeleteByID(int entityKey)
        {
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_attribute_type_del", Mapper.MapParamsForDelete(entityKey));
        }

        private void Upsert(AttributeTypeData entity)
        {
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_attribute_type_ups", Mapper.MapParamsForUpsert(entity));
        }
    }

}
