using QIQO.Monitor.SQLServer.Data;

namespace QIQO.Monitor.Api
{
    public class EnvironmentEntityService : IEnvironmentEntityService
    {
        public Environment Map(EnvironmentData ent) => new Environment(ent);

        public EnvironmentData Map(Environment ent) => new EnvironmentData
        {
            EnvironmentKey = ent.EnvironmentKey,
            EnvironmentName = ent.EnvironmentName
        };
    }
}
