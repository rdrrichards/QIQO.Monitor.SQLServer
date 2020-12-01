using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace QIQO.Monitor.Data
{
    public class CategoryRepository : RepositoryBase<CategoryData>,
                                     ICategoryRepository
    {
        private readonly IMonitorDbContext entityContext;
        public CategoryRepository(IMonitorDbContext dbc, ICategoryMap map) : base(map)
        {
            entityContext = dbc;
        }

        public override IEnumerable<CategoryData> GetAll()
        {
            using (entityContext) return MapRows(entityContext.ExecuteProcedureAsSqlDataReader("usp_category_all"));
        }

        public override CategoryData GetByID(int category_key)
        {
            var pcol = new List<SqlParameter>() { Mapper.BuildParam("@category_key", category_key) };
            using (entityContext) return MapRow(entityContext.ExecuteProcedureAsSqlDataReader("usp_category_get", pcol));
        }

        public override void Insert(CategoryData entity)
        {
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Save(CategoryData entity)
        {
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Delete(CategoryData entity)
        {
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_category_del", Mapper.MapParamsForDelete(entity));
        }

        public override void DeleteByID(int entityKey)
        {
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_category_del", Mapper.MapParamsForDelete(entityKey));
        }

        private void Upsert(CategoryData entity)
        {
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_category_ups", Mapper.MapParamsForUpsert(entity));
        }
    }
}
