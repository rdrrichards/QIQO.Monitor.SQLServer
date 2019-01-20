using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QIQO.Monitor.SQLServer.Data
{
    public class VersionMap : MapperBase, IVersionMap
    {
        public VersionData Map(IDataReader record)
        {
            try
            {
                return new VersionData()
                {
                    VersionText = NullCheck<string>(record["server_name"])
                };
            }
            catch (Exception ex)
            {
                throw new MapException($"VersionMap Exception occured: {ex.Message}", ex);
            }
        }

        public List<SqlParameter> MapParamsForUpsert(VersionData entity) => throw new NotImplementedException();

        public List<SqlParameter> MapParamsForDelete(VersionData entity) => throw new NotImplementedException();

        public List<SqlParameter> MapParamsForDelete(int server_key) => throw new NotImplementedException();
    }
}
