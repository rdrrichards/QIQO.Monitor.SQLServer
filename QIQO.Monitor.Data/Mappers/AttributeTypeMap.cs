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
                    AttributeTypeKey = NullCheck<int>(record["attribute_type_key"]),
                    AttributeDataTypeKey = NullCheck<int>(record["attribute_data_type_key"]),
                    AttributeTypeName = NullCheck<string>(record["attribute_type_name"])
                };
            }
            catch (Exception ex)
            {
                throw new MapException($"AttributeTypeData Exception occured: {ex.Message}", ex);
            }
        }

        public List<SqlParameter> MapParamsForUpsert(AttributeTypeData entity) => new List<SqlParameter>
            {
                BuildParam("@attribute_type_key", entity.AttributeTypeKey),
                BuildParam("@attribute_data_type_key", entity.AttributeDataTypeKey),
                BuildParam("@attribute_type_name", entity.AttributeTypeName),
                // GetOutParam()
            };

        public List<SqlParameter> MapParamsForDelete(AttributeTypeData entity) => MapParamsForDelete(entity.AttributeTypeKey);

        public List<SqlParameter> MapParamsForDelete(int attributeTypeKey) => new List<SqlParameter>
            {
                BuildParam("@attribute_type_key", attributeTypeKey),
                // GetOutParam()
            };
    }
}
