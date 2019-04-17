using Microsoft.Extensions.Logging;
using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace QIQO.Monitor.SQLServer.Data
{
    public class AttributeTypeRepository : RepositoryBase<AttributeTypeData>,
                                     IAttributeTypeRepository
    {
        private readonly IMonitorDbContext entityContext;
        public AttributeTypeRepository(IMonitorDbContext dbc, IAttributeTypeMap map, ILogger<AttributeTypeData> log) : base(log, map)
        {
            entityContext = dbc;
        }

        public override IEnumerable<AttributeTypeData> GetAll()
        {
            Log.LogInformation("Accessing AttributeTypeRepository GetAll function");
            using (entityContext) return MapRows(entityContext.ExecuteProcedureAsSqlDataReader("usp_attribute_type_all"));
        }

        public override AttributeTypeData GetByID(int attribute_type_key)
        {
            Log.LogInformation("Accessing AttributeTypeRepository GetByID function");
            var pcol = new List<SqlParameter>() { Mapper.BuildParam("@attribute_type_key", attribute_type_key) };
            using (entityContext) return MapRow(entityContext.ExecuteProcedureAsSqlDataReader("usp_attribute_type_get", pcol));
        }

        public override void Insert(AttributeTypeData entity)
        {
            Log.LogInformation("Accessing AttributeTypeRepository Insert function");
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Save(AttributeTypeData entity)
        {
            Log.LogInformation("Accessing AttributeTypeRepository Save function");
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Delete(AttributeTypeData entity)
        {
            Log.LogInformation("Accessing AttributeTypeRepository Delete function");
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_attribute_type_del", Mapper.MapParamsForDelete(entity));
        }

        public override void DeleteByID(int entityKey)
        {
            Log.LogInformation("Accessing AttributeTypeRepository Delete function");
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_attribute_type_del", Mapper.MapParamsForDelete(entityKey));
        }

        private void Upsert(AttributeTypeData entity)
        {
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_attribute_type_ups", Mapper.MapParamsForUpsert(entity));
        }
    }

}
