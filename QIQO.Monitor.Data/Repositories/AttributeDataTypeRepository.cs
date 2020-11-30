using Microsoft.Extensions.Logging;
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
        public AttributeDataTypeRepository(IMonitorDbContext dbc, IAttributeDataTypeMap map, ILogger<AttributeDataTypeData> log) : base(log, map)
        {
            entityContext = dbc;
        }

        public override IEnumerable<AttributeDataTypeData> GetAll()
        {
            Log.LogInformation("Accessing AttributeDataTypeRepository GetAll function");
            using (entityContext) return MapRows(entityContext.ExecuteProcedureAsSqlDataReader("usp_attribute_data_type_all"));
        }

        public override AttributeDataTypeData GetByID(int attribute_data_type_key)
        {
            Log.LogInformation("Accessing AttributeDataTypeRepository GetByID function");
            var pcol = new List<SqlParameter>() { Mapper.BuildParam("@attribute_data_type_key", attribute_data_type_key) };
            using (entityContext) return MapRow(entityContext.ExecuteProcedureAsSqlDataReader("usp_attribute_data_type_get", pcol));
        }

        public override void Insert(AttributeDataTypeData entity)
        {
            Log.LogInformation("Accessing AttributeDataTypeRepository Insert function");
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Save(AttributeDataTypeData entity)
        {
            Log.LogInformation("Accessing AttributeDataTypeRepository Save function");
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Delete(AttributeDataTypeData entity)
        {
            Log.LogInformation("Accessing AttributeDataTypeRepository Delete function");
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_attribute_data_type_del", Mapper.MapParamsForDelete(entity));
        }

        public override void DeleteByID(int entityKey)
        {
            Log.LogInformation("Accessing AttributeDataTypeRepository Delete function");
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_attribute_data_type_del", Mapper.MapParamsForDelete(entityKey));
        }

        private void Upsert(AttributeDataTypeData entity)
        {
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_attribute_data_type_ups", Mapper.MapParamsForUpsert(entity));
        }
    }

}
