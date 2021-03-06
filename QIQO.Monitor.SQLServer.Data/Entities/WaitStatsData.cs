﻿using QIQO.Monitor.Core.Contracts;

namespace QIQO.Monitor.SQLServer.Data
{
    public class WaitStatsData : IEntity
    {
        public long BatchNo { get; set; }
        public string WaitType { get; set; }
        public decimal WaitPercentage { get; set; }
        public decimal AvgWaitSec { get; set; }
        public decimal AvgResSec { get; set; }
        public decimal AvgSigSec { get; set; }
        public decimal WaitSec { get; set; }
        public decimal ResourceSec { get; set; }
        public decimal SignalSec { get; set; }
        public long WaitCount { get; set; }
    }
}