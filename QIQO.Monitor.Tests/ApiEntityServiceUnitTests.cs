using QIQO.Monitor.Api;
using QIQO.Monitor.Api.Services;
using QIQO.Monitor.Domain;
using QIQO.Monitor.SQLServer.Data;
using System;
using Xunit;

namespace QIQO.Monitor.Tests
{
    public class ApiEntityServiceUnitTests
    {
        [Fact]
        public void EnvironmentEntityServiceTest()
        {
            // Arrange
            var data = new EnvironmentData { EnvironmentKey = 1, EnvironmentName = "Test" };
            var model = new Api.Environment(data);
            // Act
            var es = new EnvironmentEntityService();
            var newModel = es.Map(data);
            var newData = es.Map(model);

            // Assert
            Assert.True(newModel.EnvironmentKey == 1);
            Assert.True(newModel.EnvironmentName == "Test");
            Assert.True(newData.EnvironmentKey == 1);
            Assert.True(newData.EnvironmentName == "Test");
        }
        [Fact]
        public void MonitorEntityServiceTest()
        {
            // Arrange
            var data = new MonitorData { MonitorKey = 1, MonitorName = "Test", MonitorTypeKey = 1 };
            var model = new Api.Monitor(data);
            // Act
            var es = new MonitorEntityService();
            var newModel = es.Map(data);
            var newData = es.Map(model);

            // Assert
            Assert.True(newModel.MonitorKey == 1);
            Assert.True(newModel.MonitorName == "Test");
            Assert.True(newModel.MonitorType == Api.MonitorType.SqlServer);
            Assert.True(newData.MonitorKey == 1);
            Assert.True(newData.MonitorName == "Test");
            Assert.True(newData.MonitorTypeKey == 1);
        }
        [Fact]
        public void ServerEntityServiceTest()
        {
            // Arrange
            var data = new ServerData { ServerKey = 1, ServerName = "Test" };
            var model = new Api.Server(data);
            // Act
            var es = new ServerEntityService();
            var newModel = es.Map(data);
            var newData = es.Map(model);

            // Assert
            Assert.True(newModel.ServerKey == 1);
            Assert.True(newModel.ServerName == "Test");
            Assert.True(newData.ServerKey == 1);
            Assert.True(newData.ServerName == "Test");
        }
        [Fact]
        public void ServiceEntityServiceTest()
        {
            // Arrange
            var data = new ServiceData
            {
                ServiceKey = 1,
                ServiceName = "Test",
                InstanceName = "I1",
                ServerKey = 1,
                ServiceSource = "Test",
                ServiceTypeKey = 1
            };
            var model = new Api.Service(data);
            // Act
            var es = new ServiceEntityService();
            var newModel = es.Map(data);
            var newData = es.Map(model);

            // Assert
            Assert.True(newModel.ServiceKey == 1);
            Assert.True(newModel.ServiceName == "Test");
            Assert.True(newModel.InstanceName == "I1");
            Assert.True(newModel.ServerKey == 1);
            Assert.True(newModel.ServiceSource == "Test");
            Assert.True(newModel.ServiceType == Api.ServiceType.SqlServer);

            Assert.True(newData.ServiceKey == 1);
            Assert.True(newData.ServiceName == "Test");
            Assert.True(newData.InstanceName == "I1");
            Assert.True(newData.ServerKey == 1);
            Assert.True(newData.ServiceSource == "Test");
            Assert.True(newData.ServiceTypeKey == 1);
        }
        [Fact]
        public void QueryEntityServiceTest()
        {
            // Arrange
            var data = new QueryData { QueryKey = 1, Name = "Test", QueryText = "SELECT" };
            var model = new Query(data);
            // Act
            var es = new QueryEntityService();
            var newModel = es.Map(data);
            var newData = es.Map(model);

            // Assert
            Assert.True(newModel.QueryKey == 1);
            Assert.True(newModel.Name == "Test");
            Assert.True(newModel.QueryText == "SELECT");
            Assert.True(newData.QueryKey == 1);
            Assert.True(newData.Name == "Test");
            Assert.True(newData.QueryText == "SELECT");
        }



        [Fact]
        public void BlockingEntityServiceTest()
        {
            // Arrange
            var data = new BlockingData
            {
                BlockerBatch = "Test",
                BlockerSid = 1,
                BlockObject = 1,
                Database = "Test",
                LockRequest = "Test",
                LockType = "Test",
                WaiterBatch = "Test",
                WaiterSid = 1,
                WaiterStatement = "Test",
                WaitTime = 1
            };
            var model = new Blocking("Test", "Test", 1, "Test", 1, 1, "Test", "Test", 1, "Test");
            // Act
            var es = new BlockingEntityService();
            var newModel = es.Map(data);

            // Assert
            Assert.True(newModel.BlockerBatch == "Test");
            Assert.True(newModel.BlockerSid == 1);
            Assert.True(newModel.BlockObject == 1);
            Assert.True(newModel.Database == "Test");
            Assert.True(newModel.LockRequest == "Test");
            Assert.True(newModel.LockType == "Test");
            Assert.True(newModel.WaiterBatch == "Test");
            Assert.True(newModel.WaiterSid == 1);
            Assert.True(newModel.WaiterStatement == "Test");
            Assert.True(newModel.WaitTime == 1);

            Assert.Throws<NotImplementedException>(() => es.Map(model));
        }
        [Fact]
        public void OpenTransactionEntityServiceTest()
        {
            // Arrange
            var data = new OpenTransactionData
            {
                DatabaseId = 1,
                DatabaseName = "Test",
                HostName = "Test",
                LoginName = "Test",
                SessionId = 1,
                TransactionBegan = new DateTime(1970, 1, 1),
                TransactionID = 1,
                TransactionName = "Test"
            };
            var model = new OpenTransaction(1, "Test", "Test", 1, "Test", new DateTime(1970, 1, 1), 1, "Test");
            // Act
            var es = new OpenTransactionEntityService();
            var newModel = es.Map(data);

            // Assert
            Assert.True(newModel.DatabaseId == 1);
            Assert.True(newModel.DatabaseName == "Test");
            Assert.True(newModel.HostName == "Test");
            Assert.True(newModel.LoginName == "Test");
            Assert.True(newModel.SessionId == 1);
            Assert.True(newModel.TransactionBegan == new DateTime(1970, 1, 1));
            Assert.True(newModel.TransactionID == 1);
            Assert.True(newModel.TransactionName == "Test");

            Assert.Throws<NotImplementedException>(() => es.Map(model));
        }
        [Fact]
        public void WaitStatsEntityServiceTest()
        {
            // Arrange
            var data = new WaitStatsData
            {
                AvgResSec = 1,
                AvgSigSec = 1,
                AvgWaitSec = 1,
                ResourceSec = 1,
                SignalSec = 1,
                WaitCount = 1,
                WaitPercentage = 1,
                WaitSec = 1,
                WaitType = "Test",
                BatchNo = 1
            };
            var model = new WaitStats(1, "Test", 1, 1, 1, 1, 1, 1, 1, 1);
            // Act
            var es = new WaitStatsEntityService();
            var newModel = es.Map(data);

            // Assert
            Assert.True(newModel.BatchNo == 1);
            Assert.True(newModel.AvgResSec == 1);
            Assert.True(newModel.AvgSigSec == 1);
            Assert.True(newModel.AvgWaitSec == 1);
            Assert.True(newModel.ResourceSec == 1);
            Assert.True(newModel.SignalSec == 1);
            Assert.True(newModel.WaitCount == 1);
            Assert.True(newModel.WaitPercentage == 1);
            Assert.True(newModel.WaitSec == 1);
            Assert.True(newModel.WaitType == "Test");

            Assert.Throws<NotImplementedException>(() => es.Map(model));
        }
    }
}
