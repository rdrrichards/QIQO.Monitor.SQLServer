﻿using QIQO.Monitor.Core.Contracts;
using QIQO.Monitor.Data;

namespace QIQO.Monitor.Client
{
    public class Environment : IModel
    {
        public Environment(EnvironmentData environmentData)
        {
            EnvironmentKey = environmentData.EnvironmentKey;
            EnvironmentName = environmentData.EnvironmentName;
        }
        public int EnvironmentKey { get; }
        public string EnvironmentName { get; }

    }
}
