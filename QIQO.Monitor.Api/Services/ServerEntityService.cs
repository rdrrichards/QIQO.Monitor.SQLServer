using QIQO.Monitor.SQLServer.Data;
using System;

namespace QIQO.Monitor.Api
{
    public class ServerEntityService : IServerEntityService
    {
        public Server Map(ServerData ent) => new Server(ent);

        public ServerData Map(Server ent) => throw new NotImplementedException();
    }
}
