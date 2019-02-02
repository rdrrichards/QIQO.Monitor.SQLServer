using QIQO.Monitor.Core;
using System;
using System.Data;

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
                    VersionText = NullCheck<string>(record["version_text"])
                };
            }
            catch (Exception ex)
            {
                throw new MapException($"VersionMap Exception occured: {ex.Message}", ex);
            }
        }
    }
}
