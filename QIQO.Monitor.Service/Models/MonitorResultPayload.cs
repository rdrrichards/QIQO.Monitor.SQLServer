//using QIQO.Monitor.Domain;
//using System;

//namespace QIQO.Monitor.Service
//{
//    public class MonitorResultPayload
//    {
//        public MonitorResultPayload(IMonitorResult monitorResult) => MonitorResult = monitorResult;
//        public MonitorResultPayload(IMonitorResult monitorResult, Exception exception) : this(monitorResult)
//        {
//            if (exception != null)
//            {
//                HasError = true;
//                Exception = exception;
//            }
//        }
//        public MonitorResultPayload(Exception exception) : this(null, exception) { }
//        public IMonitorResult MonitorResult { get; set; }
//        public bool HasError { get; set; } = false;
//        public Exception Exception { get; set; } = null;
//        public HealthStatus HealthStatus { get; set; } = HealthStatus.Healthly;
//    }
//}
