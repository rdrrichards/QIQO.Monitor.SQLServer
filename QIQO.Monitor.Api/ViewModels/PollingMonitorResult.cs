﻿using QIQO.Monitor.Domain;
using System;

namespace QIQO.Monitor.Api
{
    public class PollingMonitorResult
    {
        public PollingMonitorResult(IMonitorResult monitorResult) => MonitorResult = monitorResult;
        public PollingMonitorResult(IMonitorResult monitorResult, Exception exception) : this(monitorResult)
        {
            if (exception != null)
            {
                HasError = true;
                Exception = exception;
            }
        }
        public PollingMonitorResult(Exception exception) : this(null, exception) { }
        public IMonitorResult MonitorResult { get; }
        public bool HasError { get; } = false;
        public Exception Exception { get; } = null;
    }

    public enum MonitorCategories
    {
        Version = 1,
        SQLServerHardware,
        DetectBlocking,
        OpenTranactions,
        WaitStatistics
    }
}
