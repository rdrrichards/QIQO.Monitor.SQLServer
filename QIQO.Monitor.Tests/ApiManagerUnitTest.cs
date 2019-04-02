using Moq;
using QIQO.Monitor.Api;
using QIQO.Monitor.Api.Services;
using QIQO.Monitor.SQLServer.Data;
using System.Collections.Generic;
using Xunit;

namespace QIQO.Monitor.Tests
{
    public class ApiManagerUnitTest
    {
        [Fact]
        public void EnvironmentManagerTest()
        {
            // Arrange
            var cache = new Mock<ICoreCacheService>();
            var repo = new Mock<IEnvironmentRepository>();
            var envData = new List<EnvironmentData> { new EnvironmentData { EnvironmentKey = 1, EnvironmentName = "Test" } };
            cache.Setup(m => m.GetEnviroments()).Returns(envData);

            // Act
            var sut = new EnvironmentManager(cache.Object, repo.Object);
            var envs = sut.GetEnvironments();

            // Assert
            Assert.True(envs.Count == 1);
        }
        [Fact]
        public void ServiceManagerTest()
        {
            // Arrange
            var cache = new Mock<ICoreCacheService>();
            var qm = new Mock<IQueryEntityService>();
            var em = new Mock<IEnvironmentEntityService>();
            var envData = new List<ServiceData> { new ServiceData { ServiceKey = 1, ServiceName = "Test", InstanceName = "I1",
                ServerKey = 1, ServiceSource = "Test", ServiceTypeKey = 1 } };
            cache.Setup(m => m.GetServices()).Returns(envData);

            // Act
            var sut = new ServiceManager(cache.Object, qm.Object, em.Object);
            var envs = sut.GetServices();

            // Assert
            Assert.True(envs.Count == 1);
        }
        [Fact]
        public void ServerManagerTest()
        {
            // Arrange
            var cache = new Mock<ICoreCacheService>();
            var qm = new Mock<IQueryEntityService>();
            var em = new Mock<IEnvironmentEntityService>();
            var repo = new Mock<IServerRepository>();
            var envData = new List<ServerData> { new ServerData { ServerKey = 1, ServerName = "Test" } };
            cache.Setup(m => m.GetServers()).Returns(envData);

            // Act
            var sut = new ServerManager(cache.Object, qm.Object, em.Object, repo.Object);
            var envs = sut.GetServers();

            // Assert
            Assert.True(envs.Count == 1);
        }
    }
}
