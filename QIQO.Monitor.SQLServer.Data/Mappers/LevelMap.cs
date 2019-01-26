using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QIQO.Monitor.SQLServer.Data
{
    public class LevelMap : MapperBase, ILevelMap
    {
        public LevelData Map(IDataReader record)
        {
            try
            {
                return new LevelData()
                {
                    LevelKey = NullCheck<int>(record["level_key"]),
                    LevelName = NullCheck<string>(record["level_name"])
                };
            }
            catch (Exception ex)
            {
                throw new MapException($"LevelMap Exception occured: {ex.Message}", ex);
            }
        }

        public List<SqlParameter> MapParamsForUpsert(LevelData entity) => new List<SqlParameter>
            {
                new SqlParameter("@level_key", entity.LevelKey),
                //new SqlParameter("@audit_action", entity.AuditAction),
                GetOutParam()
            };

        public List<SqlParameter> MapParamsForDelete(LevelData entity) => MapParamsForDelete(entity.LevelKey);

        public List<SqlParameter> MapParamsForDelete(int level_key) => new List<SqlParameter>
            {
                new SqlParameter("@level_key", level_key),
                GetOutParam()
            };
    }
}
