﻿namespace QIQO.Monitor.SQLServer.Data
{
    public class QueryData : CommonData
    {
        public int QueryKey { get; set; }
        public string Name { get; set; }
        public int LevelKey { get; set; }
        public int CategoryKey { get; set; }
        public string QueryText { get; set; }

    }
}