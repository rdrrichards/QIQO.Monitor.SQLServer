using QIQO.Monitor.SQLServer.Data;
using System;

namespace QIQO.Monitor.Api
{
    public class EnvironmentEntityService : IEnvironmentEntityService
    {
        public Environment Map(EnvironmentData ent) => new Environment(ent);

        public EnvironmentData Map(Environment ent) => throw new NotImplementedException();
    }
}
