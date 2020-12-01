using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace QIQO.Monitor.Data
{
    public class AttributeDataTypeRepository : RepositoryBase<AttributeDataTypeData>,
                                    IAttributeDataTypeRepository
    {
        private readonly IMonitorDbContext entityContext;
        public AttributeDataTypeRepository(IMonitorDbContext dbc, IAttributeDataTypeMap map) : base(map)
        {
            entityContext = dbc;
        }

        public override IEnumerable<AttributeDataTypeData> GetAll()
        {
            using (entityContext) return MapRows(entityContext.ExecuteProcedureAsSqlDataReader("usp_attribute_data_type_all"));
        }

        public override AttributeDataTypeData GetByID(int attribute_data_type_key)
        {
            var pcol = new List<SqlParameter>() { Mapper.BuildParam("@attribute_data_type_key", attribute_data_type_key) };
            using (entityContext) return MapRow(entityContext.ExecuteProcedureAsSqlDataReader("usp_attribute_data_type_get", pcol));
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
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_attribute_data_type_del", Mapper.MapParamsForDelete(entity));
        }

        public override void DeleteByID(int entityKey)
        {
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_attribute_data_type_del", Mapper.MapParamsForDelete(entityKey));
        }

        private void Upsert(AttributeDataTypeData entity)
        {
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_attribute_data_type_ups", Mapper.MapParamsForUpsert(entity));
        }
    }

}
