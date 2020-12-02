using QIQO.Monitor.Data;

namespace QIQO.Monitor.Client
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
