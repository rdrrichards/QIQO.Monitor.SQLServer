using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QIQO.Monitor.Data
{
    public class AttributeTypeMap : MapperBase, IAttributeTypeMap
    {
        public AttributeTypeData Map(IDataReader record)
        {
            try
            {
                return new AttributeTypeData()
                {
                    AttributeTypeKey = NullCheck<int>(record["AttributeTypeKey"]),
                    AttributeDataTypeKey = NullCheck<int>(record["AttributeDataTypeKey"]),
                    AttributeTypeName = NullCheck<string>(record["AttributeTypeName"])
                };
            }
            catch (Exception ex)
            {
                throw new MapException($"AttributeTypeData Exception occured: {ex.Message}", ex);
            }
        }

        public List<SqlParameter> MapParamsForUpsert(AttributeTypeData entity) => new List<SqlParameter>
            {
                BuildParam("@AttributeTypeKey", entity.AttributeTypeKey),
                BuildParam("@AttributeDataTypeKey", entity.AttributeDataTypeKey),
                BuildParam("@AttributeTypeName", entity.AttributeTypeName),
                // GetOutParam()
            };

        public List<SqlParameter> MapParamsForDelete(AttributeTypeData entity) => MapParamsForDelete(entity.AttributeTypeKey);

        public List<SqlParameter> MapParamsForDelete(int attributeTypeKey) => new List<SqlParameter>
            {
                BuildParam("@AttributeTypeKey", attributeTypeKey),
                // GetOutParam()
            };
    }
}
