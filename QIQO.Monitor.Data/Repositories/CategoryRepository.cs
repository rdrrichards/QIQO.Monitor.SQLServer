﻿using Microsoft.Extensions.Logging;
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
        public CategoryRepository(IMonitorDbContext dbc, ICategoryMap map, ILogger<CategoryData> log) : base(log, map)
        {
            entityContext = dbc;
        }

        public override IEnumerable<CategoryData> GetAll()
        {
            Log.LogInformation("Accessing CategoryRepository GetAll function");
            using (entityContext) return MapRows(entityContext.ExecuteProcedureAsSqlDataReader("usp_category_all"));
        }

        public override CategoryData GetByID(int category_key)
        {
            Log.LogInformation("Accessing CategoryRepository GetByID function");
            var pcol = new List<SqlParameter>() { Mapper.BuildParam("@category_key", category_key) };
            using (entityContext) return MapRow(entityContext.ExecuteProcedureAsSqlDataReader("usp_category_get", pcol));
        }

        public override void Insert(CategoryData entity)
        {
            Log.LogInformation("Accessing CategoryRepository Insert function");
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Save(CategoryData entity)
        {
            Log.LogInformation("Accessing CategoryRepository Save function");
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Delete(CategoryData entity)
        {
            Log.LogInformation("Accessing CategoryRepository Delete function");
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_category_del", Mapper.MapParamsForDelete(entity));
        }

        public override void DeleteByID(int entityKey)
        {
            Log.LogInformation("Accessing CategoryRepository Delete function");
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_category_del", Mapper.MapParamsForDelete(entityKey));
        }

        private void Upsert(CategoryData entity)
        {
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_category_ups", Mapper.MapParamsForUpsert(entity));
        }
    }
}
