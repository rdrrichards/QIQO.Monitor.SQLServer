using Moq;
using QIQO.Monitor.Api.Controllers;
using QIQO.Monitor.Api.Services;
using QIQO.Monitor.SQLServer.Data;
using System.Collections.Generic;
using Xunit;

namespace QIQO.Monitor.Tests
{
    public class ApiControllerUnitTests
    {
        [Fact]
        public void BlockingControllerShouldCreate()
        {
            // Arrange
            var cf = new Mock<IDbContextFactory>();
            var rf = new Mock<IDataRepositoryFactory>();
            var sm = new Mock<IServiceManager>();

            // Act
            var sut = new BlockingController(cf.Object, rf.Object, sm.Object);

            // Assert
            Assert.True(sut != null);
        }
        [Fact]
        public void EnvironmentsControllerShouldCreate()
        {
            // Arrange
            var em = new Mock<IEnvironmentManager>();

            // Act
            var sut = new EnvironmentsController(em.Object);

            // Assert
            Assert.True(sut != null);
        }
        [Fact]
        public void MonitorsControllerShouldCreate()
        {
            // Arrange
            var em = new Mock<IMonitorManager>();

            // Act
            var sut = new MonitorsController(em.Object);

            // Assert
            Assert.True(sut != null);
        }
        [Fact]
        public void OpenTransactionControllerShouldCreate()
        {
            // Arrange
            var cf = new Mock<IDbContextFactory>();
            var rf = new Mock<IDataRepositoryFactory>();
            var sm = new Mock<IServiceManager>();

            // Act
            var sut = new OpenTransactionController(cf.Object, rf.Object, sm.Object);

            // Assert
            Assert.True(sut != null);
        }
        [Fact]
        public void OpsControllerShouldCreate()
        {
            // Arrange
            var em = new Mock<ICoreCacheService>();

            // Act
            var sut = new OpsController(em.Object);

            // Assert
            Assert.True(sut != null);
        }
        [Fact]
        public void QueriesControllerShouldCreate()
        {
            // Arrange
            var em = new Mock<IQueryManager>();

            // Act
            var sut = new QueriesController(em.Object);

            // Assert
            Assert.True(sut != null);
        }
        [Fact]
        public void ServersControllerShouldCreate()
        {
            // Arrange
            var em = new Mock<IServerManager>();

            // Act
            var sut = new ServersController(em.Object);

            // Assert
            Assert.True(sut != null);
        }
        [Fact]
        public void ServicesControllerShouldCreate()
        {
            // Arrange
            var em = new Mock<IServiceManager>();

            // Act
            var sut = new ServicesController(em.Object);

            // Assert
            Assert.True(sut != null);
        }
        [Fact]
        public void VersionControllerShouldCreate()
        {
            // Arrange
            var cf = new Mock<IDbContextFactory>();
            var rf = new Mock<IDataRepositoryFactory>();
            var sm = new Mock<IServiceManager>();

            // Act
            var sut = new VersionController(cf.Object, rf.Object, sm.Object);

            // Assert
            Assert.True(sut != null);
        }



        [Fact]
        public void BlockingControllerGetShouldReturnActionResult()
        {
            // Arrange
            var data = new Api.Service(new ServiceData
            {
                ServiceKey = 1,
                ServiceName = "Test",
                InstanceName = "I1",
                ServerKey = 1,
                ServiceSource = "Test",
                ServiceTypeKey = 1
            }, new List<Api.Monitor> { new Api.Monitor(new MonitorData {
                    MonitorTypeKey = 1, CategoryKey = 3, LevelKey = 1,
                    MonitorKey = 1, MonitorName = "Test"
                }, new List<Api.Query> { new Api.Query(new QueryData { QueryText = "SELECT" }) })
            }, new List<Api.Environment> { new Api.Environment(new EnvironmentData { EnvironmentKey = 1, EnvironmentName = "Test" }) });
            var cf = new Mock<IDbContextFactory>();
            var rf = new Mock<IDataRepositoryFactory>();
            var sm = new Mock<IServiceManager>();
            var repo = new Mock<IBlockingRepository>();
            sm.Setup(m => m.GetServices()).Returns(new List<Api.Service>{data});
            rf.Setup(m => m.GetDataRepository<IBlockingRepository>()).Returns(repo.Object);

            // Act
            var sut = new BlockingController(cf.Object, rf.Object, sm.Object);
            var ret = sut.Get(1);

            // Assert
            Assert.True(((Microsoft.AspNetCore.Mvc.ObjectResult)ret.Result).StatusCode == 200);
        }

        [Fact]
        public void OpenTransactionControllerGetShouldReturnActionResult()
        {
            // Arrange
            var data = new Api.Service(new ServiceData
            {
                ServiceKey = 1,
                ServiceName = "Test",
                InstanceName = "I1",
                ServerKey = 1,
                ServiceSource = "Test",
                ServiceTypeKey = 1
            }, new List<Api.Monitor> { new Api.Monitor(new MonitorData {
                    MonitorTypeKey = 1, CategoryKey = 4, LevelKey = 1,
                    MonitorKey = 1, MonitorName = "Test"
                }, new List<Api.Query> { new Api.Query(new QueryData { QueryText = "SELECT" }) })
            }, new List<Api.Environment> { new Api.Environment(new EnvironmentData { EnvironmentKey = 1, EnvironmentName = "Test" }) });
            var cf = new Mock<IDbContextFactory>();
            var rf = new Mock<IDataRepositoryFactory>();
            var sm = new Mock<IServiceManager>();
            var repo = new Mock<IOpenTransactionRepository>();
            sm.Setup(m => m.GetServices()).Returns(new List<Api.Service> { data });
            rf.Setup(m => m.GetDataRepository<IOpenTransactionRepository>()).Returns(repo.Object);

            // Act
            var sut = new OpenTransactionController(cf.Object, rf.Object, sm.Object);
            var ret = sut.Get(1);

            // Assert
            Assert.True(((Microsoft.AspNetCore.Mvc.ObjectResult)ret.Result).StatusCode == 200);
        }

        [Fact]
        public void VersionControllerGetShouldReturnActionResult()
        {
            // Arrange
            var data = new Api.Service(new ServiceData
            {
                ServiceKey = 1,
                ServiceName = "Test",
                InstanceName = "I1",
                ServerKey = 1,
                ServiceSource = "Test",
                ServiceTypeKey = 1
            }, new List<Api.Monitor> { new Api.Monitor(new MonitorData {
                    MonitorTypeKey = 1, CategoryKey = 4, LevelKey = 1,
                    MonitorKey = 1, MonitorName = "Test"
                }, new List<Api.Query> { new Api.Query(new QueryData { QueryText = "SELECT" }) })
            }, new List<Api.Environment> { new Api.Environment(new EnvironmentData { EnvironmentKey = 1, EnvironmentName = "Test" }) });
            var cf = new Mock<IDbContextFactory>();
            var rf = new Mock<IDataRepositoryFactory>();
            var sm = new Mock<IServiceManager>();
            var repo = new Mock<IVersionRepository>();
            sm.Setup(m => m.GetServices()).Returns(new List<Api.Service> { data });
            rf.Setup(m => m.GetDataRepository<IVersionRepository>()).Returns(repo.Object);
            repo.Setup(m => m.Get()).Returns(new List<VersionData> { new VersionData { VersionText = "TEST" } });

            // Act
            var sut = new VersionController(cf.Object, rf.Object, sm.Object);
            var ret = sut.Get(1);

            // Assert
            Assert.True(((Microsoft.AspNetCore.Mvc.ObjectResult)ret.Result).StatusCode == 200);
        }
    }
}
