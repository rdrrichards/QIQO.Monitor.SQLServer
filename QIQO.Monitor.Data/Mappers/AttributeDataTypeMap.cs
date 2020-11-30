using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QIQO.Monitor.Data
{
    public class AttributeDataTypeMap : MapperBase, IAttributeDataTypeMap
    {
        public AttributeDataTypeData Map(IDataReader record)
        {
            try
            {
                return new AttributeDataTypeData()
                {
                    AttributeDataTypeKey = NullCheck<int>(record["attribute_data_type_key"]),
                    AttributeDataTypeName = NullCheck<string>(record["attribute_data_type_name"])
                };
            }
            catch (Exception ex)
            {
                throw new MapException($"AttributeDataTypeMap Exception occured: {ex.Message}", ex);
            }
        }

        public List<SqlParameter> MapParamsForUpsert(AttributeDataTypeData entity) => new List<SqlParameter>
            {
                BuildParam("@attribute_data_type_key", entity.AttributeDataTypeKey),
                BuildParam("@attribute_data_type_name", entity.AttributeDataTypeName),
                // GetOutParam()
            };

        public List<SqlParameter> MapParamsForDelete(AttributeDataTypeData entity) => MapParamsForDelete(entity.AttributeDataTypeKey);

        public List<SqlParameter> MapParamsForDelete(int attributeTypeKey) => new List<SqlParameter>
            {
                BuildParam("@attribute_type_key", attributeTypeKey),
                // GetOutParam()
            };
    }
}
