using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QIQO.Monitor.Data
{
    public class CategoryMap : MapperBase, ICategoryMap
    {
        public CategoryData Map(IDataReader record)
        {
            try
            {
                return new CategoryData()
                {
                    CategoryKey = NullCheck<int>(record["CategoryKey"]),
                    CategoryName = NullCheck<string>(record["CategoryName"])
                };
            }
            catch (Exception ex)
            {
                throw new MapException($"CategoryMap Exception occured: {ex.Message}", ex);
            }
        }

        public List<SqlParameter> MapParamsForUpsert(CategoryData entity) => new List<SqlParameter>
            {
                BuildParam("@CategoryKey", entity.CategoryKey),
                BuildParam("@CategoryName", entity.CategoryName),
                // GetOutParam()
            };

        public List<SqlParameter> MapParamsForDelete(CategoryData entity) => MapParamsForDelete(entity.CategoryKey);

        public List<SqlParameter> MapParamsForDelete(int category_key) => new List<SqlParameter>
            {
                BuildParam("@CategoryKey", category_key),
                // GetOutParam()
            };
    }
}
