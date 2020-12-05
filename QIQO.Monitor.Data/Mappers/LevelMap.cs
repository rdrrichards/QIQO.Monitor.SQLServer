using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QIQO.Monitor.Data
{
    public class LevelMap : MapperBase, ILevelMap
    {
        public LevelData Map(IDataReader record)
        {
            try
            {
                return new LevelData()
                {
                    LevelKey = NullCheck<int>(record["LevelKey"]),
                    LevelName = NullCheck<string>(record["LevelName"])
                };
            }
            catch (Exception ex)
            {
                throw new MapException($"LevelMap Exception occured: {ex.Message}", ex);
            }
        }

        public List<SqlParameter> MapParamsForUpsert(LevelData entity) => new List<SqlParameter>
            {
                BuildParam("@LevelKey", entity.LevelKey),
                BuildParam("@LevelName", entity.LevelName),
                // GetOutParam()
            };

        public List<SqlParameter> MapParamsForDelete(LevelData entity) => MapParamsForDelete(entity.LevelKey);

        public List<SqlParameter> MapParamsForDelete(int level_key) => new List<SqlParameter>
            {
                BuildParam("@LevelKey", level_key),
                // GetOutParam()
            };
    }
}
