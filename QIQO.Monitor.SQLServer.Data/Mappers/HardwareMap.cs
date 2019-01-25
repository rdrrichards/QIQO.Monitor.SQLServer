using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QIQO.Monitor.SQLServer.Data
{
    public class HardwareMap : MapperBase, IHardwareMap
    {
        public HardwareData Map(IDataReader record)
        {
            try
            {
                return new HardwareData()
                {
                    ProcessPhysicalAffinity = NullCheck<string>(record["version_text"])
                };
            }
            catch (Exception ex)
            {
                throw new MapException($"HardwareMap Exception occured: {ex.Message}", ex);
            }
        }

        public List<SqlParameter> MapParamsForUpsert(HardwareData entity) => throw new NotImplementedException();

        public List<SqlParameter> MapParamsForDelete(HardwareData entity) => throw new NotImplementedException();

        public List<SqlParameter> MapParamsForDelete(int server_key) => throw new NotImplementedException();
    }
}
