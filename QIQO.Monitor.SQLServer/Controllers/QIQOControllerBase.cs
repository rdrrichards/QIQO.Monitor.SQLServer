using Microsoft.AspNetCore.Mvc;
using QIQO.Monitor.SQLServer.Data;

namespace QIQO.Monitor.SQLServer.Controllers
{
    public class QIQOControllerBase : ControllerBase
    {
        protected readonly IDbContextFactory _dbContextFactory;
        protected readonly IDataRepositoryFactory _repositoryFactory;

        public QIQOControllerBase(IDbContextFactory dbContextFactory,
            IDataRepositoryFactory repositoryFactory)
        {
            _dbContextFactory = dbContextFactory;
            _repositoryFactory = repositoryFactory;
        }
        protected string CreateConnectionString(string serverSource)
        {
            return $"Data Source={serverSource};User ID=QIQOMonitorUser;Password=QIQOMonitorUser;Application Name=QIQOMonitorAPI";
        }
        protected void CreateContext(string connectionString)
        {
            _dbContextFactory.Create(CreateConnectionString(connectionString));
        }
    }
}
