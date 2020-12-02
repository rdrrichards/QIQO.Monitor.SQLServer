using QIQO.Monitor.Data;

namespace QIQO.Monitor.Client
{
    public class ServerEntityService : IServerEntityService
    {
        public Server Map(ServerData ent) => new Server(ent);

        public ServerData Map(Server ent) => new ServerData
        {
            ServerKey = ent.ServerKey,
            ServerName = ent.ServerName
        };
    }
}
