using QIQO.Monitor.SQLServer.Data;

namespace QIQO.Monitor.Api
{
    public class MonitorEntityService : IMonitorEntityService
    {
        public Monitor Map(MonitorData ent) => new Monitor(ent);

        public MonitorData Map(Monitor ent) => new MonitorData
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
