//using Moq;
//using QIQO.Monitor.Api.Controllers;
//using QIQO.Monitor.Api.Services;
//using QIQO.Monitor.Data;
//using QIQO.Monitor.SQLServer.Data;
//using System.Collections.Generic;
//using Xunit;

//namespace QIQO.Monitor.Tests
//{
//    public class ApiControllerUnitTests
//    {
//        [Fact]
//        public void BlockingControllerShouldCreate()
//        {
//            // Arrange
//            var cf = new Mock<IDbContextFactory>();
//            var rf = new Mock<IDataRepositoryFactory>();
//            var sm = new Mock<IServiceManager>();

//            // Act
//            var sut = new BlockingController(cf.Object, rf.Object, sm.Object);

//            // Assert
//            Assert.True(sut != null);
//        }
//        [Fact]
//        public void EnvironmentsControllerShouldCreate()
//        {
//            // Arrange
//            var em = new Mock<IEnvironmentManager>();

//            // Act
//            var sut = new EnvironmentsController(em.Object);

//            // Assert
//            Assert.True(sut != null);
//        }
//        [Fact]
//        public void MonitorsControllerShouldCreate()
//        {
//            // Arrange
//            var em = new Mock<IMonitorManager>();

//            // Act
//            var sut = new MonitorsController(em.Object);

//            // Assert
//            Assert.True(sut != null);
//        }
//        [Fact]
//        public void OpenTransactionControllerShouldCreate()
//        {
//            // Arrange
//            var cf = new Mock<IDbContextFactory>();
//            var rf = new Mock<IDataRepositoryFactory>();
//            var sm = new Mock<IServiceManager>();

//            // Act
//            var sut = new OpenTransactionController(cf.Object, rf.Object, sm.Object);

//            // Assert
//            Assert.True(sut != null);
//        }
//        [Fact]
//        public void OpsControllerShouldCreate()
//        {
//            // Arrange
//            var em = new Mock<ICoreCacheService>();

//            // Act
//            var sut = new OpsController(em.Object);

//            // Assert
//            Assert.True(sut != null);
//        }
//        [Fact]
//        public void QueriesControllerShouldCreate()
//        {
//            // Arrange
//            var em = new Mock<IQueryManager>();

//            // Act
//            var sut = new QueriesController(em.Object);

//            // Assert
//            Assert.True(sut != null);
//        }
//        [Fact]
//        public void ServersControllerShouldCreate()
//        {
//            // Arrange
//            var em = new Mock<IServerManager>();

//            // Act
//            var sut = new ServersController(em.Object);

//            // Assert
//            Assert.True(sut != null);
//        }
//        [Fact]
//        public void ServicesControllerShouldCreate()
//        {
//            // Arrange
//            var em = new Mock<IServiceManager>();

//            // Act
//            var sut = new ServicesController(em.Object);

//            // Assert
//            Assert.True(sut != null);
//        }
//        [Fact]
//        public void VersionControllerShouldCreate()
//        {
//            // Arrange
//            var cf = new Mock<IDbContextFactory>();
//            var rf = new Mock<IDataRepositoryFactory>();
//            var sm = new Mock<IServiceManager>();

//            // Act
//            var sut = new VersionController(cf.Object, rf.Object, sm.Object);

//            // Assert
//            Assert.True(sut != null);
//        }



//        [Fact]
//        public void BlockingControllerGetShouldReturnActionResult()
//        {
//            // Arrange
//            var data = new Api.Service(new ServiceData
//            {
//                ServiceKey = 1,
//                ServiceName = "Test",
//                InstanceName = "I1",
//                ServerKey = 1,
//                ServiceSource = "Test",
//                ServiceTypeKey = 1
//            }, new List<Api.Monitor> { new Api.Monitor(new MonitorData {
//                    MonitorTypeKey = 1, CategoryKey = 3, LevelKey = 1,
//                    MonitorKey = 1, MonitorName = "Test"
//                }, new List<Api.Query> { new Api.Query(new QueryData { QueryText = "SELECT" }) })
//            }, new List<Api.Environment> { new Api.Environment(new EnvironmentData { EnvironmentKey = 1, EnvironmentName = "Test" }) });
//            var cf = new Mock<IDbContextFactory>();
//            var rf = new Mock<IDataRepositoryFactory>();
//            var sm = new Mock<IServiceManager>();
//            var repo = new Mock<IBlockingRepository>();
//            sm.Setup(m => m.GetServices()).Returns(new List<Api.Service> { data });
//            rf.Setup(m => m.GetDataRepository<IBlockingRepository>()).Returns(repo.Object);

//            // Act
//            var sut = new BlockingController(cf.Object, rf.Object, sm.Object);
//            var ret = sut.Get(1);

//            // Assert
//            Assert.True(((Microsoft.AspNetCore.Mvc.ObjectResult)ret.Result).StatusCode == 200);
//        }

//        [Fact]
//        public void OpenTransactionControllerGetShouldReturnActionResult()
//        {
//            // Arrange
//            var data = new Api.Service(new ServiceData
//            {
//                ServiceKey = 1,
//                ServiceName = "Test",
//                InstanceName = "I1",
//                ServerKey = 1,
//                ServiceSource = "Test",
//                ServiceTypeKey = 1
//            }, new List<Api.Monitor> { new Api.Monitor(new MonitorData {
//                    MonitorTypeKey = 1, CategoryKey = 4, LevelKey = 1,
//                    MonitorKey = 1, MonitorName = "Test"
//                }, new List<Api.Query> { new Api.Query(new QueryData { QueryText = "SELECT" }) })
//            }, new List<Api.Environment> { new Api.Environment(new EnvironmentData { EnvironmentKey = 1, EnvironmentName = "Test" }) });
//            var cf = new Mock<IDbContextFactory>();
//            var rf = new Mock<IDataRepositoryFactory>();
//            var sm = new Mock<IServiceManager>();
//            var repo = new Mock<IOpenTransactionRepository>();
//            sm.Setup(m => m.GetServices()).Returns(new List<Api.Service> { data });
//            rf.Setup(m => m.GetDataRepository<IOpenTransactionRepository>()).Returns(repo.Object);

//            // Act
//            var sut = new OpenTransactionController(cf.Object, rf.Object, sm.Object);
//            var ret = sut.Get(1);

//            // Assert
//            Assert.True(((Microsoft.AspNetCore.Mvc.ObjectResult)ret.Result).StatusCode == 200);
//        }

//        [Fact]
//        public void VersionControllerGetShouldReturnActionResult()
//        {
//            // Arrange
//            var data = new Api.Service(new ServiceData
//            {
//                ServiceKey = 1,
//                ServiceName = "Test",
//                InstanceName = "I1",
//                ServerKey = 1,
//                ServiceSource = "Test",
//                ServiceTypeKey = 1
//            }, new List<Api.Monitor> { new Api.Monitor(new MonitorData {
//                    MonitorTypeKey = 1, CategoryKey = 4, LevelKey = 1,
//                    MonitorKey = 1, MonitorName = "Test"
//                }, new List<Api.Query> { new Api.Query(new QueryData { QueryText = "SELECT" }) })
//            }, new List<Api.Environment> { new Api.Environment(new EnvironmentData { EnvironmentKey = 1, EnvironmentName = "Test" }) });
//            var cf = new Mock<IDbContextFactory>();
//            var rf = new Mock<IDataRepositoryFactory>();
//            var sm = new Mock<IServiceManager>();
//            var repo = new Mock<IVersionRepository>();
//            sm.Setup(m => m.GetServices()).Returns(new List<Api.Service> { data });
//            rf.Setup(m => m.GetDataRepository<IVersionRepository>()).Returns(repo.Object);
//            repo.Setup(m => m.Get()).Returns(new List<VersionData> { new VersionData { VersionText = "TEST" } });

//            // Act
//            var sut = new VersionController(cf.Object, rf.Object, sm.Object);
//            var ret = sut.Get(1);

//            // Assert
//            Assert.True(((Microsoft.AspNetCore.Mvc.ObjectResult)ret.Result).StatusCode == 200);
//        }
//        [Fact]
//        public void OpsControllerPingShouldReturnActionResult()
//        {
//            // Arrange
//            var em = new Mock<ICoreCacheService>();

//            // Act
//            var sut = new OpsController(em.Object);
//            var ret = sut.Ping();

//            // Assert
//            Assert.True(((Microsoft.AspNetCore.Mvc.ObjectResult)ret.Result).StatusCode == 200);
//        }
//        [Fact]
//        public void OpsControllerRefreshCacheShouldReturnActionResult()
//        {
//            // Arrange
//            var em = new Mock<ICoreCacheService>();

//            // Act
//            var sut = new OpsController(em.Object);
//            var ret = sut.RefreshCache();

//            // Assert
//            Assert.True(((Microsoft.AspNetCore.Mvc.StatusCodeResult)ret).StatusCode == 204);
//        }
//        [Fact]
//        public void EnvironmentsControllerGetShouldReturnActionResult()
//        {
//            // Arrange
//            var em = new Mock<IEnvironmentManager>();
//            em.Setup(m => m.GetEnvironments()).Returns(
//                new List<Api.Environment> { new Api.Environment(new EnvironmentData { EnvironmentName = "Test" }) });

//            // Act
//            var sut = new EnvironmentsController(em.Object);
//            var ret = sut.Get();

//            // Assert
//            Assert.True(((Microsoft.AspNetCore.Mvc.ObjectResult)ret.Result).StatusCode == 200);
//        }
//        [Fact]
//        public void MonitorsControllerGetShouldReturnActionResult()
//        {
//            // Arrange
//            var em = new Mock<IMonitorManager>();
//            em.Setup(m => m.GetMonitors()).Returns(
//                new List<Api.Monitor> { new Api.Monitor(new MonitorData { MonitorName = "Test" }) });

//            // Act
//            var sut = new MonitorsController(em.Object);
//            var ret = sut.Get();

//            // Assert
//            Assert.True(((Microsoft.AspNetCore.Mvc.ObjectResult)ret.Result).StatusCode == 200);
//        }
//        [Fact]
//        public void QueriesControllerGetShouldReturnActionResult()
//        {
//            // Arrange
//            var em = new Mock<IQueryManager>();
//            em.Setup(m => m.GetQueries()).Returns(
//                new List<Api.Query> { new Api.Query(new QueryData { Name = "Test" }) });

//            // Act
//            var sut = new QueriesController(em.Object);
//            var ret = sut.Get();

//            // Assert
//            Assert.True(((Microsoft.AspNetCore.Mvc.ObjectResult)ret.Result).StatusCode == 200);
//        }
//        [Fact]
//        public void ServersControllerGetShouldReturnActionResult()
//        {
//            // Arrange
//            var em = new Mock<IServerManager>();
//            em.Setup(m => m.GetServers()).Returns(
//                new List<Api.Server> { new Api.Server(new ServerData { ServerName = "Test" }) });

//            // Act
//            var sut = new ServersController(em.Object);
//            var ret = sut.Get();

//            // Assert
//            Assert.True(((Microsoft.AspNetCore.Mvc.ObjectResult)ret.Result).StatusCode == 200);
//        }
//        [Fact]
//        public void ServicesControllerGetShouldReturnActionResult()
//        {
//            // Arrange
//            var em = new Mock<IServiceManager>();
//            em.Setup(m => m.GetServices()).Returns(
//                new List<Api.Service> { new Api.Service(new ServiceData { ServiceName = "Test" }) });

//            // Act
//            var sut = new ServicesController(em.Object);
//            var ret = sut.Get();

//            // Assert
//            Assert.True(((Microsoft.AspNetCore.Mvc.ObjectResult)ret.Result).StatusCode == 200);
//        }



//        [Fact]
//        public void EnvironmentsControllerGetByIdShouldReturnActionResult()
//        {
//            // Arrange
//            var em = new Mock<IEnvironmentManager>();
//            em.Setup(m => m.GetEnvironments()).Returns(
//                new List<Api.Environment> { new Api.Environment(new EnvironmentData { EnvironmentKey = 1, EnvironmentName = "Test" }) });

//            // Act
//            var sut = new EnvironmentsController(em.Object);
//            var ret = sut.Get(1);

//            // Assert
//            Assert.True(((Microsoft.AspNetCore.Mvc.ObjectResult)ret.Result).StatusCode == 200);
//        }
//        [Fact]
//        public void MonitorsControllerGetByIdShouldReturnActionResult()
//        {
//            // Arrange
//            var em = new Mock<IMonitorManager>();
//            em.Setup(m => m.GetMonitors()).Returns(
//                new List<Api.Monitor> { new Api.Monitor(new MonitorData { MonitorKey = 1, MonitorName = "Test" }) });

//            // Act
//            var sut = new MonitorsController(em.Object);
//            var ret = sut.Get(1);

//            // Assert
//            Assert.True(((Microsoft.AspNetCore.Mvc.ObjectResult)ret.Result).StatusCode == 200);
//        }
//        [Fact]
//        public void QueriesControllerGetByIdShouldReturnActionResult()
//        {
//            // Arrange
//            var em = new Mock<IQueryManager>();
//            em.Setup(m => m.GetQueries()).Returns(
//                new List<Api.Query> { new Api.Query(new QueryData { QueryKey = 1, Name = "Test" }) });

//            // Act
//            var sut = new QueriesController(em.Object);
//            var ret = sut.Get(1);

//            // Assert
//            Assert.True(((Microsoft.AspNetCore.Mvc.ObjectResult)ret.Result).StatusCode == 200);
//        }
//        [Fact]
//        public void ServersControllerGetByIdShouldReturnActionResult()
//        {
//            // Arrange
//            var em = new Mock<IServerManager>();
//            em.Setup(m => m.GetServers()).Returns(
//                new List<Api.Server> { new Api.Server(new ServerData { ServerKey = 1, ServerName = "Test" }) });

//            // Act
//            var sut = new ServersController(em.Object);
//            var ret = sut.Get(1);

//            // Assert
//            Assert.True(((Microsoft.AspNetCore.Mvc.ObjectResult)ret.Result).StatusCode == 200);
//        }
//        [Fact]
//        public void ServicesControllerGetByIdShouldReturnActionResult()
//        {
//            // Arrange
//            var em = new Mock<IServiceManager>();
//            em.Setup(m => m.GetServices()).Returns(
//                new List<Api.Service> { new Api.Service(new ServiceData { ServiceKey = 1, ServiceName = "Test" }) });

//            // Act
//            var sut = new ServicesController(em.Object);
//            var ret = sut.Get(1);

//            // Assert
//            Assert.True(((Microsoft.AspNetCore.Mvc.ObjectResult)ret.Result).StatusCode == 200);
//        }



//        [Fact]
//        public void EnvironmentsControllerPostShouldReturnActionResult()
//        {
//            // Arrange
//            var newData = new Api.EnvironmentAdd { EnvironmentName = "Test" };
//            var data = new EnvironmentData { EnvironmentKey = 1, EnvironmentName = "Test" };
//            var obj = new Api.Environment(new EnvironmentData { EnvironmentKey = 1, EnvironmentName = "Test" });
//            var em = new Mock<IEnvironmentManager>();
//            em.Setup(m => m.AddEnvironment(It.IsAny<Api.EnvironmentAdd>())).Returns(obj);

//            // Act
//            var sut = new EnvironmentsController(em.Object);
//            var ret = sut.Post(newData);

//            // Assert
//            Assert.True(((Microsoft.AspNetCore.Mvc.ObjectResult)ret.Result).StatusCode == 201);
//        }
//        [Fact]
//        public void MonitorsControllerPostShouldReturnActionResult()
//        {
//            // Arrange
//            var newData = new Api.MonitorAdd { MonitorName = "Test" };
//            var data = new MonitorData { MonitorKey = 1, MonitorName = "Test" };
//            var obj = new Api.Monitor(new MonitorData { MonitorKey = 1, MonitorName = "Test" });
//            var em = new Mock<IMonitorManager>();
//            em.Setup(m => m.AddMonitor(It.IsAny<Api.MonitorAdd>())).Returns(obj);

//            // Act
//            var sut = new MonitorsController(em.Object);
//            var ret = sut.Post(newData);

//            // Assert
//            Assert.True(((Microsoft.AspNetCore.Mvc.ObjectResult)ret.Result).StatusCode == 201);
//        }
//        [Fact]
//        public void QueriesControllerPostShouldReturnActionResult()
//        {
//            // Arrange
//            var newData = new Api.QueryAdd { Name = "Test" };
//            var data = new QueryData { QueryKey = 1, Name = "Test" };
//            var obj = new Api.Query(new QueryData { QueryKey = 1, Name = "Test" });
//            var em = new Mock<IQueryManager>();
//            em.Setup(m => m.AddQuery(It.IsAny<Api.QueryAdd>())).Returns(obj);

//            // Act
//            var sut = new QueriesController(em.Object);
//            var ret = sut.Post(newData);

//            // Assert
//            Assert.True(((Microsoft.AspNetCore.Mvc.ObjectResult)ret.Result).StatusCode == 201);
//        }
//        [Fact]
//        public void ServersControllerPostShouldReturnActionResult()
//        {
//            // Arrange
//            var newData = new Api.ServerAdd { ServerName = "Test" };
//            var data = new ServerData { ServerKey = 1, ServerName = "Test" };
//            var obj = new Api.Server(new ServerData { ServerKey = 1, ServerName = "Test" });
//            var em = new Mock<IServerManager>();
//            em.Setup(m => m.AddServer(It.IsAny<Api.ServerAdd>())).Returns(obj);

//            // Act
//            var sut = new ServersController(em.Object);
//            var ret = sut.Post(newData);

//            // Assert
//            Assert.True(((Microsoft.AspNetCore.Mvc.ObjectResult)ret.Result).StatusCode == 201);
//        }
//        [Fact]
//        public void ServicesControllerPostShouldReturnActionResult()
//        {
//            // Arrange
//            var newData = new Api.ServiceAdd { ServiceName = "Test" };
//            var data = new ServiceData { ServiceKey = 1, ServiceName = "Test" };
//            var obj = new Api.Service(new ServiceData { ServiceKey = 1, ServiceName = "Test" });
//            var em = new Mock<IServiceManager>();
//            em.Setup(m => m.AddService(It.IsAny<Api.ServiceAdd>())).Returns(obj);

//            // Act
//            var sut = new ServicesController(em.Object);
//            var ret = sut.Post(newData);

//            // Assert
//            Assert.True(((Microsoft.AspNetCore.Mvc.ObjectResult)ret.Result).StatusCode == 201);
//        }



//        [Fact]
//        public void EnvironmentsControllerPutShouldReturnActionResult()
//        {
//            // Arrange
//            var newData = new Api.EnvironmentUpdate { EnvironmentName = "Test" };
//            var data = new EnvironmentData { EnvironmentKey = 1, EnvironmentName = "Test" };
//            var obj = new Api.Environment(new EnvironmentData { EnvironmentKey = 1, EnvironmentName = "Test" });
//            var em = new Mock<IEnvironmentManager>();
//            em.Setup(m => m.UpdateEnvironment(It.IsAny<int>(), It.IsAny<Api.EnvironmentUpdate>())).Returns(obj);

//            // Act
//            var sut = new EnvironmentsController(em.Object);
//            var ret = sut.Put(1, newData);

//            // Assert
//            Assert.True(((Microsoft.AspNetCore.Mvc.ObjectResult)ret.Result).StatusCode == 202);
//        }
//        [Fact]
//        public void MonitorsControllerPutShouldReturnActionResult()
//        {
//            // Arrange
//            var newData = new Api.MonitorUpdate { MonitorName = "Test" };
//            var data = new MonitorData { MonitorKey = 1, MonitorName = "Test" };
//            var obj = new Api.Monitor(new MonitorData { MonitorKey = 1, MonitorName = "Test" });
//            var em = new Mock<IMonitorManager>();
//            em.Setup(m => m.UpdateMonitor(It.IsAny<int>(), It.IsAny<Api.MonitorUpdate>())).Returns(obj);

//            // Act
//            var sut = new MonitorsController(em.Object);
//            var ret = sut.Put(1, newData);

//            // Assert
//            Assert.True(((Microsoft.AspNetCore.Mvc.ObjectResult)ret.Result).StatusCode == 202);
//        }
//        [Fact]
//        public void QueriesControllerPutShouldReturnActionResult()
//        {
//            // Arrange
//            var newData = new Api.QueryUpdate { Name = "Test" };
//            var data = new QueryData { QueryKey = 1, Name = "Test" };
//            var obj = new Api.Query(new QueryData { QueryKey = 1, Name = "Test" });
//            var em = new Mock<IQueryManager>();
//            em.Setup(m => m.UpdateQuery(It.IsAny<int>(), It.IsAny<Api.QueryUpdate>())).Returns(obj);

//            // Act
//            var sut = new QueriesController(em.Object);
//            var ret = sut.Put(1, newData);

//            // Assert
//            Assert.True(((Microsoft.AspNetCore.Mvc.ObjectResult)ret.Result).StatusCode == 202);
//        }
//        [Fact]
//        public void ServersControllerPutShouldReturnActionResult()
//        {
//            // Arrange
//            var newData = new Api.ServerUpdate { ServerName = "Test" };
//            var data = new ServerData { ServerKey = 1, ServerName = "Test" };
//            var obj = new Api.Server(new ServerData { ServerKey = 1, ServerName = "Test" });
//            var em = new Mock<IServerManager>();
//            em.Setup(m => m.UpdateServer(It.IsAny<int>(), It.IsAny<Api.ServerUpdate>())).Returns(obj);

//            // Act
//            var sut = new ServersController(em.Object);
//            var ret = sut.Put(1, newData);

//            // Assert
//            Assert.True(((Microsoft.AspNetCore.Mvc.ObjectResult)ret.Result).StatusCode == 202);
//        }
//        [Fact]
//        public void ServicesControllerPutShouldReturnActionResult()
//        {
//            // Arrange
//            var newData = new Api.ServiceUpdate { ServiceName = "Test" };
//            var data = new ServiceData { ServiceKey = 1, ServiceName = "Test" };
//            var obj = new Api.Service(new ServiceData { ServiceKey = 1, ServiceName = "Test" });
//            var em = new Mock<IServiceManager>();
//            em.Setup(m => m.UpdateService(It.IsAny<int>(), It.IsAny<Api.ServiceUpdate>())).Returns(obj);

//            // Act
//            var sut = new ServicesController(em.Object);
//            var ret = sut.Put(1, newData);

//            // Assert
//            Assert.True(((Microsoft.AspNetCore.Mvc.ObjectResult)ret.Result).StatusCode == 202);
//        }



//        [Fact]
//        public void EnvironmentsControllerDeleteShouldReturnActionResult()
//        {
//            // Arrange
//            var newData = new Api.EnvironmentUpdate { EnvironmentName = "Test" };
//            var data = new EnvironmentData { EnvironmentKey = 1, EnvironmentName = "Test" };
//            var obj = new Api.Environment(new EnvironmentData { EnvironmentKey = 1, EnvironmentName = "Test" });
//            var em = new Mock<IEnvironmentManager>();
//            em.Setup(m => m.DeleteEnvironment(It.IsAny<int>()));

//            // Act
//            var sut = new EnvironmentsController(em.Object);
//            var ret = sut.Delete(1);

//            // Assert
//            Assert.True(((Microsoft.AspNetCore.Mvc.StatusCodeResult)ret).StatusCode == 204);
//        }
//        [Fact]
//        public void MonitorsControllerDeleteShouldReturnActionResult()
//        {
//            // Arrange
//            var newData = new Api.MonitorUpdate { MonitorName = "Test" };
//            var data = new MonitorData { MonitorKey = 1, MonitorName = "Test" };
//            var obj = new Api.Monitor(new MonitorData { MonitorKey = 1, MonitorName = "Test" });
//            var em = new Mock<IMonitorManager>();
//            em.Setup(m => m.DeleteMonitor(It.IsAny<int>()));

//            // Act
//            var sut = new MonitorsController(em.Object);
//            var ret = sut.Delete(1);

//            // Assert
//            Assert.True(((Microsoft.AspNetCore.Mvc.StatusCodeResult)ret).StatusCode == 204);
//        }
//        [Fact]
//        public void QueriesControllerDeleteShouldReturnActionResult()
//        {
//            // Arrange
//            var newData = new Api.QueryUpdate { Name = "Test" };
//            var data = new QueryData { QueryKey = 1, Name = "Test" };
//            var obj = new Api.Query(new QueryData { QueryKey = 1, Name = "Test" });
//            var em = new Mock<IQueryManager>();
//            em.Setup(m => m.DeleteQuery(It.IsAny<int>()));

//            // Act
//            var sut = new QueriesController(em.Object);
//            var ret = sut.Delete(1);

//            // Assert
//            Assert.True(((Microsoft.AspNetCore.Mvc.StatusCodeResult)ret).StatusCode == 204);
//        }
//        [Fact]
//        public void ServersControllerDeleteShouldReturnActionResult()
//        {
//            // Arrange
//            var newData = new Api.ServerUpdate { ServerName = "Test" };
//            var data = new ServerData { ServerKey = 1, ServerName = "Test" };
//            var obj = new Api.Server(new ServerData { ServerKey = 1, ServerName = "Test" });
//            var em = new Mock<IServerManager>();
//            em.Setup(m => m.DeleteServer(It.IsAny<int>()));

//            // Act
//            var sut = new ServersController(em.Object);
//            var ret = sut.Delete(1);

//            // Assert
//            Assert.True(((Microsoft.AspNetCore.Mvc.StatusCodeResult)ret).StatusCode == 204);
//        }
//        [Fact]
//        public void ServicesControllerDeleteShouldReturnActionResult()
//        {
//            // Arrange
//            var newData = new Api.ServiceUpdate { ServiceName = "Test" };
//            var data = new ServiceData { ServiceKey = 1, ServiceName = "Test" };
//            var obj = new Api.Service(new ServiceData { ServiceKey = 1, ServiceName = "Test" });
//            var em = new Mock<IServiceManager>();
//            em.Setup(m => m.DeleteService(It.IsAny<int>()));

//            // Act
//            var sut = new ServicesController(em.Object);
//            var ret = sut.Delete(1);

//            // Assert
//            Assert.True(((Microsoft.AspNetCore.Mvc.StatusCodeResult)ret).StatusCode == 204);
//        }
//    }
//}
