using QIQO.Monitor.Data;

namespace QIQO.Monitor.Client
{
    public class MonitorEntityService : IMonitorEntityService
    {
        public MonitorModel Map(MonitorData ent) => new MonitorModel(ent);

        public MonitorData Map(MonitorModel ent) => new MonitorData
        {
            MonitorKey = ent.MonitorKey,
            MonitorName = ent.MonitorName,
            MonitorTypeKey = (int)ent.MonitorType,
            CategoryKey = (int)ent.MonitorCategory,
            LevelKey = (int)ent.MonitorLevel
        };
    }
    //public class MonitorPropertiesEntityService : IMonitorPropertiesEntityService
    //{
    //    public MonitorProperty Map(ServiceMonitorAttributeData serviceMonitorAttributeData,
    //        AttributeTypeData attributeTypeData, AttributeDataTypeData attributeDataTypeData)
    //    {
    //        return new MonitorProperty(attributeTypeData.AttributeTypeName,
    //            attributeDataTypeData.AttributeDataTypeName, serviceMonitorAttributeData.AttributeValue);
    //    }
    //}
}
