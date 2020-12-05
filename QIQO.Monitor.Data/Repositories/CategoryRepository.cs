using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace QIQO.Monitor.Data
{
    public class CategoryRepository : RepositoryBase<CategoryData>,
                                     ICategoryRepository
    {
        private readonly IMonitorDbContext _entityContext;
        public CategoryRepository(IMonitorDbContext dbc, ICategoryMap map) : base(map)
        {
            _entityContext = dbc;
        }

        public override IEnumerable<CategoryData> GetAll()
        {
            using (_entityContext) return MapRows(_entityContext.ExecuteProcedureAsSqlDataReader("monMonitorCategoryGetAll"));
        }

        public override CategoryData GetByID(int categoryKey)
        {
            var pcol = new List<SqlParameter>() { Mapper.BuildParam("@CategoryKey", categoryKey) };
            using (_entityContext) return MapRow(_entityContext.ExecuteProcedureAsSqlDataReader("monMonitorCategoryGet", pcol));
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
            using (_entityContext) _entityContext.ExecuteProcedureNonQuery("monMonitorCategoryDelete", Mapper.MapParamsForDelete(entity));
        }

        public override void DeleteByID(int entityKey)
        {
            using (_entityContext) _entityContext.ExecuteProcedureNonQuery("monMonitorCategoryDelete", Mapper.MapParamsForDelete(entityKey));
        }

        private void Upsert(CategoryData entity)
        {
            using (_entityContext) _entityContext.ExecuteProcedureNonQuery("monMonitorCategoryUpsert", Mapper.MapParamsForUpsert(entity));
        }
    }
}
