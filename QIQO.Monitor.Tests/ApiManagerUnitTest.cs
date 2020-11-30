//using Microsoft.Extensions.Logging;
//using Moq;
//using QIQO.Monitor.Api;
//using QIQO.Monitor.Api.Services;
//using QIQO.Monitor.Data;
//using System.Collections.Generic;
//using Xunit;

//namespace QIQO.Monitor.Tests
//{
//    public class ApiManagerUnitTests
//    {
//        [Fact]
//        public void EnvironmentManagerTest()
//        {
//            // Arrange
//            var cache = new Mock<ICoreCacheService>();
//            var repo = new Mock<IEnvironmentRepository>();
//            var es = new Mock<IEnvironmentEntityService>();
//            var logger = new Mock<ILogger<EnvironmentManager>>();
//            var envData = new List<EnvironmentData> { new EnvironmentData { EnvironmentKey = 1, EnvironmentName = "Test" } };
//            cache.Setup(m => m.GetEnvironments()).Returns(envData);

//            // Act
//            var sut = new EnvironmentManager(logger.Object, cache.Object, repo.Object, es.Object);
//            var envs = sut.GetEnvironments();

//            // Assert
//            Assert.True(envs.Count == 1);
//        }
//        [Fact]
//        public void ServiceManagerTest()
//        {
//            // Arrange
//            var cache = new Mock<ICoreCacheService>();
//            var mm = new Mock<IMonitorManager>();
//            var em = new Mock<IEnvironmentManager>();
//            var repo = new Mock<IServiceRepository>();
//            var logger = new Mock<ILogger<ServiceManager>>();
//            var envData = new List<ServiceData> { new ServiceData { ServiceKey = 1, ServiceName = "Test", InstanceName = "I1",
//                ServerKey = 1, ServiceSource = "Test", ServiceTypeKey = 1 } };
//            cache.Setup(m => m.GetServices()).Returns(envData);

//            // Act
//            var sut = new ServiceManager(logger.Object, cache.Object, repo.Object, mm.Object, em.Object);
//            var envs = sut.GetServices();

//            // Assert
//            Assert.True(envs.Count == 1);
//        }
//        [Fact]
//        public void ServerManagerTest()
//        {
//            // Arrange
//            var cache = new Mock<ICoreCacheService>();
//            var em = new Mock<IEnvironmentManager>();
//            var repo = new Mock<IServerRepository>();
//            var sm = new Mock<IServiceManager>();
//            var logger = new Mock<ILogger<ServerManager>>();
//            var envData = new List<ServerData> { new ServerData { ServerKey = 1, ServerName = "Test" } };
//            cache.Setup(m => m.GetServers()).Returns(envData);

//            // Act
//            var sut = new ServerManager(logger.Object, cache.Object, em.Object, repo.Object, sm.Object);
//            var envs = sut.GetServers();

//            // Assert
//            Assert.True(envs.Count == 1);
//        }
//        [Fact]
//        public void QueryManagerTest()
//        {
//            // Arrange
//            var cache = new Mock<ICoreCacheService>();
//            var qm = new Mock<IQueryEntityService>();
//            var repo = new Mock<IQueryRepository>();
//            var logger = new Mock<ILogger<QueryManager>>();
//            var envData = new List<QueryData> { new QueryData { QueryKey = 1, Name = "Test" } };
//            cache.Setup(m => m.GetQueries()).Returns(envData);

//            // Act
//            var sut = new QueryManager(logger.Object, cache.Object, qm.Object, repo.Object);
//            var envs = sut.GetQueries();

//            // Assert
//            Assert.True(envs.Count == 1);
//        }
//        [Fact]
//        public void MonitorManagerTest()
//        {
//            // Arrange
//            var cache = new Mock<ICoreCacheService>();
//            var es = new Mock<IMonitorEntityService>();
//            var qm = new Mock<IQueryManager>();
//            var repo = new Mock<IMonitorRepository>();
//            var logger = new Mock<ILogger<MonitorManager>>();
//            var envData = new List<MonitorData> { new MonitorData { MonitorKey = 1, MonitorName = "Test" } };
//            cache.Setup(m => m.GetMonitors()).Returns(envData);

//            // Act
//            var sut = new MonitorManager(logger.Object, cache.Object, es.Object, repo.Object, qm.Object);
//            var envs = sut.GetMonitors();

//            // Assert
//            Assert.True(envs.Count == 1);
//        }
//    }
//}
